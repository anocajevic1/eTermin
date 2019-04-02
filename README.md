# Grupa4-FFA

## Tema: eTermin

### Članovi tima

* Poljčić Faris
* Šišić Faris
* Noćajević Amna

### Opis teme
---

eTermin je aplikacija koja Vam omogućava pregled i rezervisanje terena za sve vrste sportova. Sa većinom lokacija terena vjerovatno niste ni upoznati i ova Vam aplikacija tu može pomoći. Kroz naš jednostavan sistem, korisnicima je omogućen jednostavniji pregled i uplata slobodnih termina. Naš sistem će Vam prilagoditi izbor terena prema odabranom vremenskom terminu i lokaciji. Dovoljno je da unesete željeno vrijeme i aplikacija će Vam pronaći odgovarajuće termine.

### Procesi
---

#### Registracija korisnika, izbor između guest i prijavljenog korisnika aplikacije

Novi korisnik aplikacije ima mogućnost prijave kao neregistrovani korisnik (guest) ili mogućnost registracije. Guest korisnici imaju pristup osnovnim funkcionalnostima aplikacije, kao što su pregled lokacija terena i rasporeda termina. Odabirom opcije "Login as guest" korisnik preskače prijavu i prikazuje mu se glavni prozor. Korisnik može odabrati i opciju registrovanja na početnoj formi unoseći svoje ime, prezime, šifru, jedinstveno korisničko ime i email. Nakon potvrde validnosti mail-a korisnik postaje registrovan i omogućena mu je prijava. Prijavljeni korisnici dobijaju dodatne funkcionalnosti kao što su rezervacija terena korištenjem virtualnog novca ili pregled dosadašnjih rezervacija.

#### Rezervacije termina

Rezervacija terena je omogućena prijavljenim korisnicima na dva načina. Prvi je ručnim odabirom željenog terena i termina. Korisniku se prikazuje mapa sa unesenim terenima. On odabere željeni teren nakon čega mu se prikazuju dostupni termini. Nakon izbora slobodnog termina, te nakon provjere stanja virtualnog novca, teren se rezerviše. Korisniku se umanjuje količina virtualnog novca, dok se odabrani termin označava kao zauzet. Drugi način je da korisnik odabere željeno vrijeme i dan za rezervaciju, nakon čega se automatski odabira najpogodniji teren. Sistem pretražuje sve unesene terene i slobodne termine, te predlaže najbliži teren korisniku. Ako je zadovoljan izborom korisnik potvrđuje rezervaciju, ukoliko nije, predlaže mu se sljedeći slobodni.

#### Kupovina / prodaja virtualnog novca

Korisnik će moći u svakoj poslovnici kupiti željeni iznos virtualnog novca koje će koristiti u svrhu rezervacije terena po svojim željama. Nakon izvršene uplate u gotovini, uposlenik prebacuje određen iznos virtualnog novca na račun korisnika. Kroz ovaj sistem osigurani su i korisnik i uposlenici. Lažne rezervacije će biti spriječene od strane neregistriranih korisnika, dok će registrovani korisnici morati koristiti kupljeni virtualni novac za rezervisanje. Administrator je jedini koji ima mogućnost dodavanja terena tako da korisnik može biti siguran u validnost terena i termina.

#### Dodavanje / brisanje terena

Aplikacija omogućava dodavanje novih, kao i brisanje postojećih terena, ukoliko bude potrebe za tim. Ovaj proces je bitan jer je moguća izgradnja novih terena koji žele koristiti ovu aplikaciju i zbog mogućeg raskidanja ugovora sa nekim od terena koji trenutno koriste aplikaciju, te oni bivaju uklonjeni iz ponude. Mogućnost dodavanja ili brisanja terena ima isključivo administrator aplikacije. Ukoliko se odluči za to, otvara se formular za unos podataka za novi teren. Nakon unesenih podataka vrši se validacija podataka, te konačno dodavanje u sistem. Ukoliko se odluči za brisanje, traži se potvrda, nakon potvrđivanja teren se uklanja.

#### Ažuriranje korisničkog računa

Svi neregistrovani korisnici, da bi dobili mogućnost rezervacije termina, moraju da registruju svoj korisnički račun popunjavanjem formulara i validacije istog. Tokom korištenja aplikacije, može se javiti potreba za ažuriranjem ličnih podataka registrovanog korisnika. Ova mogućnost će biti podržana. Registrovani korisnik može da ažurira svoje podatke (kao što su ime, prezime, šifra računa i slično). To može da uradi klikom na dugme za ažuriranje korisničkog računa, nakon čega se otvara formular sa podacima koji su već u upotrebi i korisnik ima mogućnost dodavanja, izmijene ili brisanja podataka. Nakon toga se vrši validacija unesenih podataka, te konačna izmjena ukoliko su ti podaci validni.

### Funkcionalnosti
---

__Neregistrovani korisnik__:
* Mogućnost registracije
* Mogućnost pregleda lokacija terena
*	Mogućnost pregleda rasporeda termina

__Registrovani korisnik__:
*	Mogućnost prijave na sistem
*	Mogućnost pregleda lokacija terena
*	Mogućnost pregleda rasporeda termina
*	Mogućnost rezervacije termina
*	Mogućnost kupovine i korištenja virtualnog novca
*	Mogućnost pregleda dosadašnjih rezervacija

__Uposlenik__:
*	Mogućnost prodaje virtualnog novca registrovanim korisnicima
*	Mogućnost manualnog rezervisanja / otkazivanja termina (npr. Rezervacija telefonom)
*	Mogućnost praćenja statistike radnog mjesta

__Administrator__:
*	Mogućnost dodavanja, izmjene i brisanja terena
*	Mogućnost pregleda, izmjene i brisanja korisnika
*	Mogućnost ažuriranja aplikacije
*	Mogućnost praćenja kompletne statistike
*	Mogućnost praćenja historije svih transakcija

### Akteri
---

* **Neregistrovani korisnik (guest user)** - ima mogućnost pregleda lokacija terena i svih termina, ali ne i njihovo rezervisanje

* **Registrovani korisnik** - može sve što i neregistrovani korisnik uz dodatne mogućnosti rezervacije korištenjem virtualnog novca, pregleda dosadašnjih rezervacija

* **Uposlenik** - ima mogućnost dodjele virtualnog novca korisnicima kao i manualno rezervisanje / otkazivanje termina

* **Administrator sistema** - ima zadatak da nadgleda sistem, uz mogućnost dodavanja terene, pregleda svih korisnika i historije transakcija, ažuriranja aplikacije, upravljanja bazom itd.
