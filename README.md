# dotnet-web-api-example
Simple Web API example using REST, CRUD and EF

# How to use
Build and run tests:
`dotnet test --project Books.Tests`
Build and run:
`dotnet run --project Books.WebApi`
Test with curl:
`curl --location --insecure --request GET 'http://localhost:5000/api/books/'`
`curl --location --request POST 'https://localhost:5000/api/books' \
--header 'Content-Type: application/json' \
--data-raw '{"Title": "Some awesome book", "not expected": "foo"}'`