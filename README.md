# ProgettoModernConcurrent
Ticketytack! - Istruzioni per l'avvio

Ticketytack! è un sito con interfaccia sviluppata in Angular2 per la prenotazione di concerti musicali in cui è possibile:
- Inserire, modificare ed eliminare sia utenti che concerti.
- Prenotare concerti, associando ad ogni utente uno o più concerti.
- Visualizzare i concerti prenotati.

Il progetto si basa sulle soluzioni "ServiceAPI" e "SuperCoolApp", che sono state rivisitate ed arricchite.

Affinchè il tutto funzioni sono necessari i seguenti software:
- Visual Studio (all'atto dell'installazione non dimenticare di aggiungere lo sviluppo per Web ASP.NET)
- Microsoft .NET Core
- Node.js
- XAMPP (per instanziare i server su cui creare i database)
- Postman (per testare le chiamate REST)
- Git
- TortoiseGit (o software equivalenti per clonare il repository)

Passaggi:
- Clonare questa repository su una cartella a scelta (Tramite TortoiseGit o equivalenti).
- Avviare XAMPP e avviare i server Apache e MySQL.
- Avviare Visual Studio ed aprire la prima soluzione "Service API" (file->apri soluzione->cartella scelta->serviceAPI->serviceAPI.sln)
- Eseguire senza debug. Verrà visualizzato un terminale che dirà che la serviceAPI è in ascolto su http://localhost:5000.
- Per evitare problemi di avvii multipli in prove successive, il database non viene creato all'avvio del programma, bisognerà bensì effettuare una chiamata GET tramite Postman. Aprire Postman ed eseguire una chiamata GET sull'indirizzo http://localhost:5000/api/setup.
- A questo punto il database è stato creato e le sue tabelle inizializzate. E' possibile verificarlo andando su phpMyAdmin (da XAMPP, nella riga di MySQL, cliccare su "Admin"). Sarà possibile vedere il database "app" in cui vi sono tre tabelle: users, concerts e associations. Le prime due contengono i dati relativi agli utenti ed ai concerti, mentre la terza serve da ponte fra le prime due, creando associazioni in caso di concerti prenotati. Da phpMyAdmin è altresì possibile creare degli Utenti, Concerti ed Associazioni d'esempio.
- E' possibile testare le RESTful API con altre chiamate tramite Postman:
Elenco chiamate:
GET http://localhost:5000/api/setup -> Crea il Database o verifica che sia stato creato se esiste già.
GET http://localhost:5000/api/users -> Ritorna l'elenco completo in formato JSON degli utenti.
GET http://localhost:5000/api/user?id=ID -> Ritorna l'utente avente come id ciò che viene specificato in "ID".
GET http://localhost:5000/api/user?psw=password -> Ritorna l'utente avente come password ciò che viene specificato in "password".
PUT http://localhost:5000/api/users -> Crea un Utente (specificare il body in formato JSON).
Es.: {
	"name":"Gino",
	"surname":"Caggeggi",
	"age":22,
	"password":"psswrd"
     }

POST http://localhost:5000/api/users -> Modifica un Utente (specificare il body in formato JSON).
DELETE http://localhost:5000/api/users?id=ID -> Elimina l'utente avente per id quello specificato.

GET http://localhost:5000/api/concerts -> Ritorna l'elenco completo in formato JSON dei Concerti.
GET http://localhost:5000/api/concert?id=ID -> Ritorna il concerto avente come id ciò che viene specificato in "ID".
PUT http://localhost:5000/api/concerts -> Crea un Concerto (specificare il body in formato JSON).
Es.: {
	"name":"Rock in Rome",
	"where":"Roma",
	"when":"15 June 2018"
     }

POST http://localhost:5000/api/concerts -> Modifica un Concerto (specificare il body in formato JSON).
DELETE http://localhost:5000/api/concerts?id=ID -> Elimina il concerto avente per id quello specificato.

GET http://localhost:5000/api/associations -> Ritorna l'elenco completo in formato JSON delle associazioni.
GET http://localhost:5000/api/associations?id=ID -> Ritorna l'associazione avente come id ciò che viene specificato in "ID".
GET http://localhost:5000/api/associationss?uid=UID -> Ritorna tutte e sole le associazioni in cui il campo uId è uguale a "UID".
PUT http://localhost:5000/api/associations -> Crea un'Associazione (specificare il body in formato JSON).
Es.: {
	"uid":2,
	"cid":5
     }

POST http://localhost:5000/api/associations -> Modifica un'Associazione (specificare il body in formato JSON).
DELETE http://localhost:5000/api/associations?id=ID -> Elimina l'associazione avente per id quello specificato.

- Avviare da Visual Studio la seconda soluzione "SuperCoolApp".
- Eseguire senza Debug. Si aprirà il Browser che visualizzerà la pagina iniziale di Ticketytack!
- Nella pagina iniziale sono semplicemente elencate le funzionalità dell'interfaccia. Per testare l'interfaccia, usare il menù affianco.
- Cliccando su "Our Customers" sarà possibile interagire con la tabella degli utenti: premendo sul pulsantino "+" si creerà un nuovo slot utente che è possibile riempire con le info desiderate con il form affianco. Cliccando su utenti già creati sarà possibile modificarli modificando il form, ed infine è possibile cancellarli premendo il pulsante "delete" relativo all'utente che si desidera eliminare.
ATTENZIONE: nessuna modifica al database sarà resa persistente se, PRIMA di abbandonare la pagina, non si clicca sul pulsante "SAVE" in basso a destra.
- Cliccando su "Concert List" saranno disponibili funzioni analoghe a quelle viste per gli utenti, ma per i concerti. Accanto ad ogni concerto, oltre al già visto pulsante "delete", saranno disponibili una casella di testo ed un pulsante "Book". Inserendo nella casella di testo la password di un utente e cliccando su "Book", verrà creata un associazione fra l'utente avente la password inserita e il concerto relativo. L'utente avrà quindi prenotato quel concerto.
- Cliccando su "My Concerts" sarà possibile visualizzare la lista dei concerti prenotati dai vari utenti. Inserendo nella casella di testo la password dell'utente desiderato e cliccando su "Show" sarà visualizzata la lista dei concerti prenotati da quell'utente.

PS.: Si noti che il progetto allo stato attuale è lontano dalla perfezione. Sono infatti presenti i seguenti problemi:
- Il formato dei concerti appartenenti ad ogni utente viene visualizzato tramite la sua stringa JSON, e non separando gli opportuni campi.
Trattasi di un problema di scope delle variabili e di parsing che non permettono di associare alla lista dei JSON la lista dei Concerti.
- Quando si prenota un concerto, la password viene inserita dentro tutte la caselle di testo.
Non è stato possibile eseguire debugging e correzioni a causa dei vincoli temporali sulla consegna del progetto stesso.

Andrea Scala - Matr. O55000337