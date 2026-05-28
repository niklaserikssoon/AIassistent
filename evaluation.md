# 📊 Evaluation Report

## 📌 Overview

Kort sammanfattning av projektet och vad som utvärderas.

**Projekt Namn:**
`Ai-assistent`

---

# 🎯 Objectives

## Vad skulle byggas?
- LLMProxy — ansvarar för att hantera prompts och kommunikation med Groq AI för att generera svar och frågor.
- ContentAPI — ansvarar för att hantera data genom CRUD-funktionalitet (Create, Read, Update, Delete).

## Vilket problem försöker vi lösa?

Projektet försöker lösa funktionen att kombinera AI genererad data med en väl strukturerad backend lösning

- tydliga separationer mellan AI logiken och data hanteringen
- effektiv kommunikation mellan tjänsterna
- säkra och organiserade CRUD funktioner
* Vad var success criteria?

## Vad var success criteria?

- ✅ LLMProxy kan kommunicera korrekt med Groq AI
- ✅ ContentAPI kan hantera CRUD-operationer utan fel
- ✅ Båda tjänsterna kan kommunicera stabilt med varandra
- ✅ API-anrop returnerar korrekt data och statuskoder
---

# 🛠️ Methodology

Projektet utvecklades med en microservice-arkitektur där varje tjänst hade ett tydligt ansvar.

Två separata tjänster skapades:

LLMProxy
- Hanterar kommunikation med Groq AI
- Skickar prompts och tar emot AI-genererade svar
ContentAPI
- Hanterar lagring och CRUD-operationer
- Exponerar endpoints för att skapa, läsa, uppdatera och ta bort data

Båda tjänsterna kommunicerar genom HTTP-anrop mellan API.

Evaluationen genomfördes genom att testa kommunikationen mellan tjänsterna och verifiera att varje funktion fungerade korrekt.
och detta gjordes lättast i Scalar/swagger

Följande tester utfördes:

- Test av LLMproxy kommunikationen till contentAPI
- Test av AI-svar från Groq AI
- Test av CRUD-funktionalitet i ContentAPI
- Verifiering av HTTP-statuskoder
- Test av felhantering och ogiltiga requests


---

# 📂 Test

## Prompt tests 

```json
[
  {
    "id": 1,
    "promt": "vad äter björnar ?",
    "category": "djur",
    "generatedText": "Björnar är omnivorer, vilket betyder att de äter både växtdelar och animaliska födoämnen. Deras kost består av flera olika saker, som:\n\n1. Frukter och bär: Björnar äter många olika slags frukter och bär, så som björnbär, blåbär och hasselnötter.\n2. Nötter: Björnar älskar nötter, så som hasselnötter, ekollon och valnötter.\n3. Insekter: Björnar äter också insekter, såsom gräshoppor och skalbaggar.\n4. Fisk: Vissa björnar, särskilt den europeiska brunbjörnen, äter ibland fisk och andra vattenlevande djur.\n5. Landlevande djur: Björnar äter även små lemminger, som hare, mört och fåglar, liksom kadaver av större djur och andra döda djur.\n6. Fruktskott och ungnötter: Efter fruktningen av vissa träd, som ek, kan björnar få tillgång till många ungnötter, som är en rikaste källa till protein och fett.\n\nI jaktvårtsförhållanden och i naturen kan en björn dricka upp till 40 liter vatten om dagen.\n\nDet är lätt att se hur en björn kan ha stor användning för i vilda gräsmarker och i landskap med stora skogsområden.",
    "createdAt": "2026-05-28T07:58:59.5951348Z"
  },
  {
    "id": 2,
    "promt": "vilken fågel klarar flyga längst?",
    "category": "djur",
    "generatedText": "Enligt Guinness Rekordbok är den fågel som klarar flyga längst den arten Alkoven. Men, den fågel som vanligtvis är associerad med att flyga längst är Arctic Ternen.\n\nArctic Ternen är en vadarfågel som är känd för att flyga längst sträcka i hela sin livscykel, ibland upp till 70 000 kilometer (43 500 mil) på en enda årsresa från sitt födelningssätt i Arktis till sin övervintringsområde i Antarktis och tillbaka igen.\n\nMen, den individuella fågeln, som utsändes från en satellit-farkost, Albatrossen, av namnet \"BD\"  flygde  51,9 dagar med en total av sträcka på 50.1 million kilometers",
    "createdAt": "2026-05-28T07:59:47.2370638Z"
  },
  {
    "id": 3,
    "promt": "Vilken är den snabbaste bilen idag?",
    "category": "fordon",
    "generatedText": "Den snabbaste bilen idag varierar beroende på typ av fordon och kriterier för snabbhet. Men så här är några av de snabbaste bilarna idag:\n\n1. **Bugatti Chiron Super Sport 300+**: Denna superbils uppnådde en hastighet på 490 km/h (304 mph) 2019 vid en provkörning. Den är utrustad med en 8,0-liters W16-motor som producerar 1 600 hästar.\n2. **Hennessey Venom F5**: Denna amerikanska superbil kan uppnå en hastighet på 435 km/h (270 mph). Den är utrustad med en 7,4-liters V8-motor som producerar 1 600 hästar.\n3. **Koenigsegg Agera RS**: Denna svenska superbil kan uppnå en hastighet på 410 km/h (255 mph). Den är utrustad med en 5,0-liters V8-motor som producerar 1 340 hästar.\n\nObservera att dessa hastigheter är provade under specifika förhållanden och att det kan finnas ännu snabbare fordon på marknaden eller under utveckling. Dessutom är hastigheten inte den enda faktorn som avgör vilken bil som är bäst, även säkerhet, hanterbarhet och miljöpåverkan är viktiga att överväga.",
    "createdAt": "2026-05-28T08:04:16.5777419Z"
  }
]
{
  "id": 4,
  "promt": "om jag har 3 äpplen och tar bort 5, hur många har jag?",
  "category": "räkna",
  "generatedText": "Det verkar som det är omöjligt att ha -2 äpplen, eftersom du inte kan ta bort mer äpplen än du har. Men om vi förutsätter att du vill ha ett logiskt resultat, skulle svaret vara -2. Du har 3 äpplen och tar bort 5, så du har minus 2 äpplen kvar.",
  "createdAt": "2026-05-28T08:54:18.4811899Z"
}
{
  "id": 5,
  "promt": "hur många kalorier har en hamburgare ?",
  "category": "mat",
  "generatedText": "Kaloriinnehållet i en hamburgare varierar beroende på ingredienserna, storleken på hamburgaren och hur den kokas. Här är ett ungefärligt kaloriinnehåll för olika typer av hamburgare:\n\n- En vanlig hamburger med bröd, ost, tomatsalsa och majonnäs kan innehålla mellan 300-400 kalorier.\n- En större eller mer fyllig hamburger kan innehålla mellan 500-700 kalorier.\n- En hamburgare med extra tillbehör som friterad kyckling, bacon eller extra ost kan innehålla mellan 800-1 200 kalorier.\n- En veggie-hamburgare eller en lågkvalitets-hamburgare kan innehålla mellan 200-300 kalorier.\n\nDet är viktigt att notera att dessa är ungefärliga uppskattningar och att kaloriinnehållet kan variera beroende på hur den tillverkats och varifrån ingredienserna kommer.\n\nFör att ge dig ett mer precis svar kan jag nämna några exempel på hamburgare från ett flertal olika restauranger, och med det antagande att de största portionerna är ingerade: \n\n- McDonald's Cheeseburger: 300 kalorier\n- Burger King Whopper med ost: 640 kalorier\n- Five Guys Bacon Cheeseburger: 1 040 kalorier",
  "createdAt": "2026-05-28T09:00:27.8126133Z"
}
```

**Förväntat resultat:**
Groq bör svara relevant till frågan som ställts

**Resultat:**
Korrekt sammanhang till frågan med ett väl genererat svar

**Begränsningar**
AI modeller är tränade för att göra användaren nöjd oavsätt hur svaret formuleras, dom har även svårt att acceptera att dom gjort fel ibland då kan dom börja hallicunera och 
nästan börja vända användarens svar till att de är deras eget.
exempel är på Prompten med id 5, generellt sätt ett bra svar men sammtidigt väldigt vagt och fel på flera ställen. hur många kalorier har en hamburgare är lite som att fråga hur långt ett snöre är.
hur stor är den ? vad innehåller den ? hur många skivor kött ? vad för såser? och modellen nämner även dessa saker men ändå kommer med exempel på kalorier, och detta är för att användaren ska bli nöjd och få 
något form av svar på sin fråga men egentligen är de inget svar om man tänker efter.

**Kan man göra annorlunda?**
Eftersom AI som nämnt ovan är designade att göra användaren nöjd är de extremt viktigt att frågan är bra formulerad och väl genomtänkt också.
att fråga hur många kalorier en hamburgare har är också en väldigt vag och dålig fråga och då förväntas ett vagt och dåligt svar.
Tänk igenom frågan noggrant innan den ställs , ta med detaljer. för att den frågan ska egentligen få ett "korrekt" svar så behöver
AI veta hur många gram kött ? vilket bröd ? vad för såser och hur mycket av varje sås ? detaljerna i frågan ger kvalitén på svaret.

**Status:** ✅ Pass

# 🧠 Sammanfattning

i helhet gör projektet det som de ska och frväntas av. LLmproxy och contentAPI pratar med varande korrekt , Promten genererar ett relevant svar förhållande till frågan samt att
CRUD funktionerna funkar som dem ska.

