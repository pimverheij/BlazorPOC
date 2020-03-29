# demo nieuwe tech
Dit is een poging een stel leuke nieuwe technologien aan elkaar te koppelen.
Ik wil daarme ervaring opdoen met alternatieve technologien
maar ook een voorbeeld softewarearchitectuur opzetten op basis waarvan we kunnen kiezen

**let op** in dit project zit ook een zelfstandige backend solution verstopt (Backend\ValidatieBackend)

## technieken die ik tot nu toe gebruik heb:
.net core
asp.net core
blazor
fluentvalidation

highlights:
### SharedModels project
herbruikbare modellen + bijbehorende validaties, code op 1 plek gebruikt in front en backend
complexe en toch leesbare validatie op postcode (afhankelijk van opgegeven land) werkt ook in frontend

### SharedModelsTest project
unittests op validaties (do I need to say more?)

### BlazorCore project
zelfstandige applicatie-onafhankelijke herbruikbare frontend-componenten
Dit is als je ze zelf maakt, maar je kan ze natuurlijk ook (bij-)kopen

aantal interesant foefjes in RsInputText:
* door trucje met overerving en naamgeving van class razor-pagina opgeknipt in opmaak en code-behind. Kan in de toekomst waarschijnlijk met partial classes. Nu nog bepalen of dit echt fijn is.
* is eigenlijk een poging om de eigenlijk applicaties simpeler te maken door herhalingen in component te stoppen, zie Adres.razor om te zien hoe goed dat werkt.
* Denk dat er nog wel iets te halen is, met defaults voor label en validatie, maar of het de moeite waard is?
* Ik zou eigenlijk ook de CSS onderdeel van het component willen laten zijn. iig de sturende CSS (zoals bij adres de actief/inactief class), maar wil vast ook iets anders dan CSS (less? sass?) ben ene beetje out of my depth hier.

_imports.razor is wel grappig, die had ik eerst neit door. Die zet eigenlij standaard een heleboel usings op alle razor-files. Schoon, maar verraderlijk lastig als je dat net door hebt, een component overzet naar een andere library en het werkt ineens niet meer!

### ValidatiePOC
interessantse aan dit project is vooral hoe weinig er nog in zit. Ik heb zoveel mogelijk herbruikbare shit in libraries gezet, dit moet pure functionaliteit zijn. Vind ik al best aardig gelukt!

fluentvalidator is gelukkig ook geschikt gemaakt voor blazor, daarvoor gebruik ik https://github.com/ryanelian/FluentValidation.Blazor (via NuGet). Dat werkt echt heel soepel.
Het kan met injection, maar ik heb een hekel aan veel specifieke injection-regels in je startup.js (en als ik het goed begrijp heb je er een nodig vor elke pagina). Dus gebruik ik nu:
'''C#
<FluentValidator Validator="validator"></FluentValidator>
'''

en in de 'code behind':
'''C#
private AdresValidator validator = new AdresValidator();
'''

Dat regelt eigenlijk de hele client-side validatie!





##todo:
experimentele unit-tests van blazor-components aan de praat krijgen
https://chrissainty.com/introduction-to-blazor-component-testing/
