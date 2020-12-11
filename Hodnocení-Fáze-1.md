**Hodnotenie: 44 bodov**

**Git**
- OK.

**Čistota kódu**
- [x] Blokové komentáre na začiatku v testovacích triedach - veľmi zastaralá praktika, na toto slúži verzovací systém. Niektoré informácie sú tam úplne zbytočné, máte to komentované v angličtine aj češtine…
- [x] Pomenovanie konštánt - vhodnejšie je asi použiť Pascal case, ale môžete byť aj Upper case, ale slová by som kvôli čitateľnosti oddelil znakom "\_".
- [x] Veľké množstvo zbytočných komentárov.
- [x] GetAll_Should_return_Proffessional_and_Graphic_design - aký je toto casing? Prečo niektoré slová máte lower case a zvyšné upper case?
- [x] Zakomentovaný kód nepatrí do remote repozitára, minimálne nie do master branch.

**Testy**
- CategoryControllerTests - aký má zmysel mať tie 3 testy v časti "GetAll Tests"? Nestačil by GetAll_Should_return_Proffessional_and_Graphic_design? A prečo máte v Asserte XXX.Should().HaveCountGreaterOrEqualTo(2);? Nevráti to vždy 2 záznamy?
- V GetById_Should_return_something testoch by som očakával, že skontrolujete aj dáta, ktoré vám daný endpoint vracia. Nie len to, že vám príde status code 200 a že deserializovaný objekt nie je prázdny.
- Aký má zmysel v GetById_With_empty_Id testoch kontrolovať, že response nie je null?
- Vo viacerých testoch máte TODO komentáre s tým, že vám testy padajú, aj keď všetky pri spustení prechádzajú.
- Mať v testoch viac Act a Assert častí je zvláštne a horšie čitateľné.
- Prečo v Search_Should_find_two_products a Search_Should_find_manufacturer porovnávate vrátené hodnoty s propertami z XXXNewModel? Endpoint vám vracia XXXListModel a keby ste to porovnávali s týmto modelom, tak v teste, kde pracujete s produktami odhalíte problém uvedený nižšie (Splnenie funkcionality).
	
**Splnenie funkcionality**
- [ ] Search vám vracia zlé výsledky. Myslel som si, že je problém niekde v implementácií vyhľadávania, ale problém je v  hodnote property EntityTypeEnum v ProductEntity. 

**Ostatné**
- Pre propertu Weight by bolo asi lepšie použiť nejaké desatinné číslo, ale ak to chcete reprezentovať napr. v gramoch tak je to OK.
- Mohli ste viac využiť dedičnosť u modelov - napr. ak máte rovnaké property a validačné pravidlá/chybové hlášky, tak ich môžete vyextrahovať do bázovej triedy (new a update model).
- Vaše kategórie reprezentujú typové určenie počítačov aj ich cenovú hladinu? Predpokladal by som, že výber ceny bude riešený inak.
