# AI-assistent API

detta projekt är en AI assistent som samverkar med två olika API:er samtidigt , ena pratar med AI(ollama)
och den andra gör själva logiken som att spara och kunna filtrera frågorna i historik.

### 🚀 Hur man testar projektet

1. Klona repot från GitHub genom att kopiera länken.

2. Öppna Visual Studio.

3. Klicka på **"Clone Repository"** och klistra in länken.

4. Projektet består av två API:er i samma solution, så båda måste startas.

5. Öppna två terminaler:

   - I första terminalen:
     - cd contentapi
     - dotnet run
   - I andra terminalen:
     - cd llmproxy
     - dotnet run

## 🔐 Konfigurera API-nyckel (User Secrets)

Detta projekt använder en API-nyckel för att säkra kommunikationen mellan tjänsterna.

### 1. Initiera User Secrets

Navigera till projektmappen (ContentAPI):
  - cd ContentAPI
  - dotnet user-secrets init

2. Lägg till API-nyckel

Kör följande kommando:
  - dotnet user-secrets set "ApiSettings:InternalApiKey" "din-hemliga-nyckel"
  - Exempel på nyckel: A5*51:)sH?p?2^ac6&

3. Upprepa för LLMproxy
  - cd ../LLMproxy
  - dotnet user-secrets init
  - dotnet user-secrets set "ApiSettings:InternalApiKey" "din-hemliga-nyckel"

⚠️ Viktigt:
API-nyckeln måste vara samma i båda projekten för att kommunikationen ska fungera.
