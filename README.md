# Project Scenario: Library Book Management System

### Project Overview
Create a web application to manage a library's book inventory. <br/>
The application will allow users to perform CRUD (Create, Read, Update, Delete) operations on books in the library.

### Requirements
#### Book Entity: <br/>

BookID: int (Primary Key) <br/>
Title: string <br/>
Author: string <br/>
ISBN: string <br/>
PublishedDate: DateTime <br/>
Genre: string <br/>


### CRUD Operations:
Create: Add a new book to the library. <br/>
Read: View a list of all books and view details of a specific book. <br/>
Update: Edit details of an existing book. <br/>
Delete: Remove a book from the library. <br/>

### Views:
Index: Display a list of all books with options to create a new book, edit, or delete existing books. <br/>
Create: Form to add a new book. <br/>
Edit: Form to edit an existing book's details. <br/>
Details: Display details of a specific book. <br/>
Delete: Confirmation page to delete a book. <br/>

### Validation:
Ensure that all fields are required. <br/>
Validate that the ISBN is in the correct format. <br/>
Ensure the PublishedDate is not in the future. <br/>

### Additional Features (Optional)
Search Functionality: Implement a search feature to filter books by title or author. <br/>
Pagination: Add pagination to the list of books. <br/>
User Authentication: Add user authentication and authorization to restrict access to certain actions. <br/>
