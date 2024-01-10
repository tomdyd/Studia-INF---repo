using mailSender;

Pracownik pracownik = new Pracownik("Tomasz", "Dyda", 27, "t.dyda@aerosol.pl", Stanowisko.Dyrektor);

Pracownik[] pracowniks = new Pracownik[]
{
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek1@gmail.com", Stanowisko.Kadrowy),
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek2@gmail.com", Stanowisko.Kadrowy),
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek3@gmail.com", Stanowisko.Kadrowy),
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek4@gmail.com", Stanowisko.Kadrowy),
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek5@gmail.com", Stanowisko.Kadrowy),
    new Pracownik("Ryszard", "Gnojek", 27, "r.gnojek6@gmail.com", Stanowisko.Kadrowy)
};

MS ms = new MS();
Sekretariat sekretariat = new Sekretariat(ms);

sekretariat.Send(pracownik, pracowniks, "Widomosc tekstowa");