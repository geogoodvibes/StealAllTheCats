**Steal All the Cats!**

**Περιγραφή**

Αυτό το project είναι μια ASP.NET  Core  Web  API εφαρμογή, που στοχεύει στη "κλοπή" εικόνων γατών από το **Cats**  **as**  **a**  **Service**  **API** ([thecatapi.com](https://thecatapi.com/)) και στην αποθήκευσή τους στη βάση δεδομένων. Η λύση περιλαμβάνει endpoints για την ανάκτηση, την αποθήκευση και την οργάνωση των εικόνων των γατών, καθώς και των χαρακτηριστικών τους (tags). Έχει υλοποιηθεί με **.****NET** **8** και χρησιμοποιεί είτε **Microsoft**  **SQL**  **Server** είτε **in****-****memory** βάση δεδομένων για την αποθήκευση των δεδομένων.

**Προαπαιτούμενα**

Για να εκτελέσετε την εφαρμογή, θα χρειαστείτε:

-   .NET 8 SDK
-   Microsoft SQL Server (ή επιλογή για in-memory database)
-   Entity Framework Core

**Οδηγίες εγκατάστασης**

1.  **Clone**  **repo**

git clone https://github.com/geogoodvibes/StealAllTheCats.git

2.  **Ρύθμιση βάσης δεδομένων**Το project είναι ρυθμισμένο για χρήση με **Microsoft**  **SQL**  **Server**. Μπορείτε είτε να συνδέσετε ένα υπάρχον SQL  Server  instance είτε να χρησιμοποιήσετε in-memory βάση δεδομένων. Ελέγξτε το αρχείο appsettings.json και τροποποιήστε τις ρυθμίσεις σύνδεσης (ConnectionStrings) στο δικό σας SQL  Server.

Εάν προτιμάτε να χρησιμοποιήσετε SQL  Server:

"ConnectionStrings": {

"DefaultConnection":  "DefaultConnection": "Server=localhost,1433;Database=CatsDb;User ID=sa;Password=Tsirigot@ki1987!;TrustServerCertificate=true"

}

Εάν θέλετε να χρησιμοποιήσετε in-memory βάση δεδομένων, απλώς ρυθμίστε το στο Startup.cs όπως παρακάτω:

services.AddDbContext< StealAllTheCatsContext >(options =>

options.UseInMemoryDatabase("StealAllTheCatsDb "));

Θα βρείτε σχολιασμένο τον κώδικα σε δύο σημεία μέσα στο project, μπορείτε να το ξεσχολιάσετε και να το γυρίσετε σε InMemoryDB.

3.  **Ενημέρωση Βάσης Δεδομένων** Χρησιμοποιήστε το Entity  Framework  Core για να δημιουργήσετε τη βάση δεδομένων: (το path να βρίσκεται στο StealThecats.API  project)

dotnet ef database update

4.  **Εκκίνηση της εφαρμογής**  
    Για να ξεκινήσετε την εφαρμογή:

dotnet run

Ή απλώς Run απο το Visual Studio.

5.  **Swagger**  **API**  **Documentation**Για να δείτε την τεκμηρίωση του API, ανοίξτε το [https://localhost:7256/swagger](https://localhost:7256/swagger) στον browser σας.

**API Endpoints**

**POST /api/cats/fetch**

Κατεβάζει εικόνες από το **CaaS**  **API** και τις αποθηκεύει στη βάση δεδομένων. Αν το endpoint επανεκτελεστεί, δεν αποθηκεύονται διπλές εγγραφές. Μπορείτε να επιλέξετε πόσες γάτες θέλετε να προστεθούν συνολικά, αλλάζοντας την παράμετρο.

**GET** **/****api****/****cats****/{****id****}**

Ανακτά μια εικόνα βάσει του μοναδικού της ID.

**GET /api/cats**

Ανακτά paged  list με εικόνες. Χρησιμοποιήστε παραμέτρους page και pageSize για να καθορίσετε τη σελίδα και το μέγεθός της. Παράδειγμα:  
GET /api/cats?page=1&pageSize=10

**GET /api/cats?tag={tag}**

Ανακτά εικόνες που έχουν συγκεκριμένο tag, με υποστήριξη σελιδοποίησης. Παράδειγμα:  
GET /api/cats?tag=playful&page=1&pageSize=10

**Λεπτομέρειες υλοποίησης**

Όταν επιλέξετε να κάνετε fetch τα δεδομένα, παρακαλώ περιμένετε λίγο, καθώς η διαδικασία εκτελείται κανονικά. Μην ανησυχείτε, **λειτουργεί!**

Για λόγους ευκολίας, τα αρχεία αποθηκεύονται στον τρέχοντα φάκελο, δηλαδή στο working  directory του API  project, το οποίο βρίσκεται στη διαδρομή:  
~\StealAllTheCats\StealAllTheCats.API\Cat  Photos\

Δεδομένου ότι οι εικόνες μπορούν να μοιρασθούν απευθείας από τον server, θα μπορούσαμε απλά να αποθηκεύουμε το URL και να το καλούμε κάθε φορά που το χρειαζόμαστε. Ωστόσο, επειδή στο μέλλον μπορεί οι εικόνες να μην είναι διαθέσιμες, τις αποθηκεύουμε σε ένα τοπικό φάκελο. Θεωρητικά, αυτός ο φάκελος θα μπορούσε να είναι ένας δικτυακός χώρος ή αποθηκευτικός χώρος αρχείων. Σε αυτή την περίπτωση, για λόγους ευκολίας, αποθηκεύουμε τις εικόνες στο τρέχον directory.

Σε σχέση με τα ζητούμενα της άσκησης, προστέθηκε η μέθοδος DownloadFile, ώστε να υπάρχει ένας τρόπος να επιβεβαιωθεί ότι το fetch των φωτογραφιών ολοκληρώθηκε σωστά. Μετά το execute, πατήστε Download File.

**Τεχνολογίες**

-   **ASP****.****NET**  **Core** **8** για την ανάπτυξη του Web  API
-   **Entity**  **Framework**  **Core** για την αλληλεπίδραση με τη βάση δεδομένων
-   **SQL Server** (ή in-memory βάση δεδομένων)
-   **Swagger** για την τεκμηρίωση του API

**Βασικές Αρχές και Λειτουργικότητα**

Η εφαρμογή ακολουθεί βασικές αρχές της αρχιτεκτονικής REST και έχει υλοποιηθεί με στόχο να είναι επεκτάσιμη και εύκολη στη συντήρηση. Έχουν επίσης υλοποιηθεί κανόνες επικύρωσης των δεδομένων που εξασφαλίζουν ότι δεν καταχωρούνται διπλές εγγραφές στη βάση δεδομένων.

Επιπλέον, η λύση περιλαμβάνει δυνατότητα για **paging** και αναζήτηση γατών με βάση το tag τους, επιτρέποντας την εύκολη διαχείριση μεγάλου όγκου δεδομένων.

**xUnit Tests**

• FetchCats_ReturnsBadRequest_WhenExceptionThrown - Εξασφαλίζει ότι η μέθοδος FetchCats επιστρέφει Bad Request όταν προκύψει εξαίρεση κατά την προσθήκη γατών στη βάση δεδομένων.

• GetCat_ReturnsOk_WhenCatExists - Επαληθεύει ότι η μέθοδος GetCat επιστρέφει 200 OK όταν υπάρχει μια γάτα με το συγκεκριμένο ID.

• GetCat_ReturnsNotFound_WhenCatDoesNotExist - Εξασφαλίζει ότι η μέθοδος GetCat επιστρέφει 404 Not Found όταν η ζητούμενη γάτα δεν υπάρχει στη βάση δεδομένων.

• GetCatsByTag_ReturnsOk_WhenCatsExist - Δοκιμάζει ότι η μέθοδος GetCatsByTag επιστρέφει 200 OK όταν υπάρχουν γάτες που αντιστοιχούν στην ετικέτα.

• GetCatsByTag_ReturnsNotFound_WhenNoCatsExist - Εξασφαλίζει ότι η μέθοδος GetCatsByTag επιστρέφει 404 Not Found όταν δεν βρεθούν γάτες με την παρεχόμενη ετικέτα.

• DownloadFileAsync_ReturnsNotFound_WhenImageDoesNotExist - Επικυρώνει ότι η μέθοδος DownloadFileAsync επιστρέφει 404 Not Found αν η εικόνα της γάτας δεν υπάρχει στον διακομιστή.

• FetchCats_ReturnsBadRequest_WhenInvalidCatCount - Δοκιμάζει ότι η μέθοδος FetchCats επιστρέφει 400 Bad Request όταν παρέχεται μη έγκυρος αριθμός γατών (αρνητικός).


**Running Tests**

dotnet test

