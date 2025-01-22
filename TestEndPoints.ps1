
# This script is used to test the endpoints
param(
    [string]$environment = "Development",
    [string]$launchProfile = "https-Development",
    [string]$connectionStringKey = "Development2",
    [bool]$dropDatabase = $true,
    [bool]$createDatabase = $true
)

# Get the project name
$projectName = Get-ChildItem -Recurse -Filter "*.csproj" | Select-Object -First 1 | ForEach-Object { $_.Directory.Name } # Projectname can also be set manually

# Get the base URL of the project
$launchSettings = Get-Content -LiteralPath ".\$projectName\Properties\launchSettings.json" | ConvertFrom-Json
$baseUrl = ($launchSettings.profiles.$launchProfile.applicationUrl -split ";")[0] # Can also set manually -> $baseUrl = "https://localhost:7253"

#Install module SqlServer
if (-not (Get-Module -ErrorAction Ignore -ListAvailable SqlServer)) {
    Write-Verbose "Installing SqlServer module for the current user..."
    Install-Module -Scope CurrentUser SqlServer -ErrorAction Stop
}
Import-Module SqlServer

# Set the environment variable
$env:ASPNETCORE_ENVIRONMENT = $environment



# Read the connection string from appsettings.Development.json
$appSettings = Get-Content ".\$projectName\appsettings.$environment.json" | ConvertFrom-Json
$connectionString = $appSettings.ConnectionStrings.$connectionStringKey
Write-Host "Database Connection String: $connectionString" -ForegroundColor Blue


# Get the database name from the connection string
if ($connectionString -match "Database=(?<dbName>[^;]+)")
{
    $databaseName = $matches['dbName']
    Write-Host "Database Name: $databaseName" -ForegroundColor Blue
}else{
    Write-Host "Database Name not found in connection string" -ForegroundColor Red
    exit
}


# Check if the database exists
$conStringDbExcluded = $connectionString -replace "Database=[^;]+;", ""
$queryDbExists = Invoke-Sqlcmd -ConnectionString $conStringDbExcluded -Query "Select name FROM sys.databases WHERE name='$databaseName'"
if($queryDbExists){
    if($dropDatabase -or (Read-Host "Do you want to drop the database? (y/n)").ToLower() -eq "y") { 

        # Drop the database
        Invoke-Sqlcmd -ConnectionString $connectionString -Query  "USE master;ALTER DATABASE $databaseName SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE $databaseName;"
        Write-Host "Database $databaseName dropped." -ForegroundColor Green
    }
}

# Create the database from the model
if(Select-String -LiteralPath ".\$projectName\Program.cs" -Pattern "EnsureCreated()"){
    Write-Host "The project uses EnsureCreated() to create the database from the model." -ForegroundColor Yellow
} else {
    if($createDatabase -or (Read-Host "Should dotnet ef migrate and update the database? (y/n)").ToLower() -eq "y") { 

        dotnet ef migrations add "UpdateModelFromScript_$(Get-Date -Format "yyyyMMdd_HHmmss")" --project ".\$projectName\$projectName.csproj"
        dotnet ef database update --project ".\$projectName\$projectName.csproj"
    }
}

# Run the application
if((Read-Host "Start the server from Visual studio? (y/n)").ToLower() -ne "y") { 
    Start-Process -FilePath "dotnet" -ArgumentList "run --launch-profile $launchProfile --project .\$projectName\$projectName.csproj" -WindowStyle Normal    
    Write-Host "Wait for the server to start..." -ForegroundColor Yellow 
}

# Continue with the rest of the script
Read-Host "Press Enter to continue when the server is started..."



### =============================================================
### =============================================================
### =============================================================

# Send requests to the API endpoint




### Copy below code to test the endpoints


# ----- Testing Post Endpoints ----- # 
Write-Host "`nTesting POST endpoints"
$httpMethod = "Post"   ### "Get", "Post", "Put", "Delete"

Write-Host "`nCreating authors"
$endPoint = "$baseUrl/api/Authors"

# Array of authors to be posted
$authors = @(
    [PSCustomObject]@{FirstName = "F. Scott"; LastName = "Fitzgerald"},
    [PSCustomObject]@{FirstName = "George"; LastName = "Orwell"},
    [PSCustomObject]@{FirstName = "Harper"; LastName = "Lee"},
    [PSCustomObject]@{FirstName = "J.D."; LastName = "Salinger"},
    [PSCustomObject]@{FirstName = "Jane"; LastName = "Austen"}
    )
# Posting the above authors
foreach ($author in $authors) {
    try {
        # Convert each author object to JSON
        $authorJson = $author | ConvertTo-Json -Depth 1

        # Send the JSON to the API via POST request
        $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $authorJson -ContentType "application/json"

        # Print the response (or handle it as needed)
        Write-Host "Successfully posted: $($author.FirstName, $author.LastName)"
    }
    catch {
        Write-Host "Failed to post $($author.FirstName, $author.LastName): $_"
    }
}

#----------#
Write-Host "`nCreating books"
$endPoint = "$baseUrl/api/Books"

$books = @(
    [PSCustomObject]@{Title = "The Great Gatsby"; ISBN = "978-0743273565"; ReleaseYear = 1925; Rating = 4.3},
    [PSCustomObject]@{Title = "1984"; ISBN = "978-0451524935"; ReleaseYear = 1949; Rating = 4.7},
    [PSCustomObject]@{Title = "To Kill a Mockingbird"; ISBN = "978-0061120084"; ReleaseYear = 1960; Rating = 4.8},
    [PSCustomObject]@{Title = "The Catcher in the Rye"; ISBN = "978-0316769488"; ReleaseYear = 1951; Rating = 4.1},
    [PSCustomObject]@{Title = "Pride and Prejudice"; ISBN = "978-1503290563"; ReleaseYear = 1813; Rating = 4.5}
    )

foreach ($book in $books) {
    try {
        $bookJson = $book | ConvertTo-Json -Depth 1

        $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $bookJson -ContentType "application/json"

        Write-Host "Successfully posted: $($book.Title)"
    }
    catch {
        Write-Host "Failed to post $($book.Title): $_"
    }
}

#----------#
Write-Host "`nCreating LoanTakers(?)/LoanCardOwners, also gives them new loan cards"
$endPoint = "$baseUrl/api/LoanCardOwners"

$loanCardOwners = @(
    [PSCustomObject]@{FirstName = "Mari"; LastName = "Iochi"},
    [PSCustomObject]@{FirstName = "Kuchiki"; LastName = "Byakuya"},
    [PSCustomObject]@{FirstName = "Mickey"; LastName = "Mouse"},
    [PSCustomObject]@{FirstName = "Luke"; LastName = "Skyrunner"},
    [PSCustomObject]@{FirstName = "John"; LastName = "Snowedin"}
)

foreach ($loanCardOwner in $loanCardOwners) {
    try {
        $loanCardOwnerJson = $loanCardOwner | ConvertTo-Json -Depth 1

        $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $loanCardOwnerJson -ContentType "application/json"

        Write-Host "Successfully posted: $($loanCardOwner.LastName)"
    }
    catch {
        Write-Host "Failed to post $($loanCardOwner.LastName): $_"
    }
}

#----------#
Write-Host "`nCreating Loans"
$endPoint = "$baseUrl/api/Loans"

$loans = @(
    [PSCustomObject]@{BookId = 1; LoanCardId = 1},
    [PSCustomObject]@{BookId = 2; LoanCardId = 2},
    [PSCustomObject]@{BookId = 3; LoanCardId = 3},
    [PSCustomObject]@{BookId = 4; LoanCardId = 4},
    [PSCustomObject]@{BookId = 5; LoanCardId = 5},
    [PSCustomObject]@{BookId = 1; LoanCardId = 3}
)

foreach ($loan in $loans) {
    try {
        $loanJson = $loan | ConvertTo-Json -Depth 1

        $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $loanJson -ContentType "application/json"

        Write-Host "Successfully posted: $($loan.BookId, $loan.LoanCardId)"
    }
    catch {
        Write-Host "Failed to post $($loan.BookId, $loan.LoanCardId): $_"
    }
}



# ----- Testing Put Endpoints ----- # 
$httpMethod = "Put"   ### "Get", "Post", "Put", "Delete"

Write-Host "`nReturning Loans (Gives loans returned date and sets the books IsAvailable = true)"
$endPoint = "$baseUrl/api/Loans"

$idArray = 1..3

foreach ($id in $idArray) {
    try {
        $endPoint = "$baseUrl/api/Loans/return/$id"

        $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -ContentType "application/json"

        Write-Host "Successfully posted: Book with Id $($id)"
    }
    catch {
        Write-Host "Failed to post: Book with Id $($id): $_"
    }
}

#----------#
Write-Host "`nAssigning one Author to one Book"
$endPoint = "$baseUrl/api/Books/1/assign-authors"

$authorIds = @(1)
try {
    $authorIdsJson = @{ "authorIds" = $authorIds } | ConvertTo-Json -Depth 1

    $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $authorIdsJson -ContentType "application/json"

    Write-Host "Successfully assigned authors: Author with Id $($authorIds)"

}
catch {
    Write-Host "Failed to put: Author with Id $($authorIds) Error: $($_.Exception.Message)"
}

Write-Host "`nAssigning multiple Authors to one Book"
$endPoint = "$baseUrl/api/Books/2/assign-authors"

$authorIds = @(1, 2, 3)
try {
    $authorIdsJson = @{ "authorIds" = $authorIds } | ConvertTo-Json -Depth 1

    $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $authorIdsJson -ContentType "application/json"

    Write-Host "Successfully assigned authors: Authors with Id $($authorIds[0], ", ", $authorIds[1], ", ", $authorIds[2])"

}
catch {
    Write-Host "Failed to put: Author with Id $($authorIds[0], ", ", $authorIds[1], ", ", $authorIds[2]) Error: $($_.Exception.Message)"
}


Write-Host "`nAssigning one Book to one Author"
$endPoint = "$baseUrl/api/Authors/4/assign-books"

$bookIds = @(3)
try {
    $bookIdsJson = @{ "bookIds" = $bookIds } | ConvertTo-Json -Depth 1

    $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $bookIdsJson -ContentType "application/json"

    Write-Host "Successfully assigned book: Book with Id $($bookIds)"

}
catch {
    Write-Host "Failed to put: Book with Id $($bookIds) Error: $($_.Exception.Message)"
}

Write-Host "`nAssigning multiple Books to one Author"
$endPoint = "$baseUrl/api/Authors/5/assign-books"

$bookIds = @(4, 5)
try {
    $bookIdsJson = @{ "bookIds" = $bookIds } | ConvertTo-Json -Depth 1

    $response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -Body $bookIdsJson -ContentType "application/json"

    Write-Host "Successfully assigned authors: Authors with Id $($bookIds[0], ", ", $bookIds[1], ", ", $bookIds[2])"

}
catch {
    Write-Host "Failed to put: Author with Id $($bookIds[0], ", ", $bookIds[1], ", ", $bookIds[2]) Error: $($_.Exception.Message)"
}




# ----- Testing Get Endpoints ----- # 
$httpMethod = "Get"   ### "Get", "Post", "Put", "Delete"

Write-Host "`nGetting all books"
$endPoint = "$baseUrl/api/Books"

$response = Invoke-RestMethod -Uri $endPoint -Method $httpMethod -ContentType "application/json"
$response | Format-Table

#----------#
Write-Host "`nGetting one book"
$endPoint = "$baseUrl/api/Books/1"

$response = Invoke-RestMethod -Uri $endPoint -Method  $httpMethod -ContentType "application/json"
$response | Format-Table



# ----- Testing Delete Endpoints ----- # 
$httpMethod = "Delete"   ### "Get", "Post", "Put", "Delete"

Write-Host "`nDeleting one Book"
$endPoint = "$baseUrl/api/Books/1"

$response = Invoke-WebRequest -Uri $endPoint -Method $httpMethod -Body ($body | ConvertTo-Json) -ContentType "application/json"
# Checks response status code
if ($response.StatusCode -eq 204) {
    Write-Host "Request was successful. No content returned (HTTP 204)."
}
else {
    Write-Host "Request failed. Status code: $($response.StatusCode)"
}

#----------#
Write-Host "`nDeleting one Author"
$endPoint = "$baseUrl/api/Authors/1"

$response = Invoke-WebRequest -Uri $endPoint -Method $httpMethod -Body ($body | ConvertTo-Json) -ContentType "application/json"

if ($response.StatusCode -eq 204) {
    Write-Host "Request was successful. No content returned (HTTP 204)."
}
else {
    Write-Host "Request failed. Status code: $($response.StatusCode)"
}

#----------#
Write-Host "`nDeleting one LoanCardOwner,`nBecause of Cascading delete this users Loans are also deleted"
$endPoint = "$baseUrl/api/LoanCardOwners/1"

$response = Invoke-WebRequest -Uri $endPoint -Method $httpMethod -Body ($body | ConvertTo-Json) -ContentType "application/json"

if ($response.StatusCode -eq 204) {
    Write-Host "Request was successful. No content returned (HTTP 204)."
}
else {
    Write-Host "Request failed. Status code: $($response.StatusCode)"
}





### ------------ Query Tables from the database
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM Author"
$sqlResult | Format-Table
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM Book"
$sqlResult | Format-Table
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM AuthorBook"
$sqlResult | Format-Table

$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM LoanCardOwner"
$sqlResult | Format-Table
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM LoanCard"
$sqlResult | Format-Table
$sqlResult = Invoke-Sqlcmd -ConnectionString $connectionString -Query "Select * FROM Loan"
$sqlResult | Format-Table
