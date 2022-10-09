# Moq sample

Implementing console application applything everything I learn while watching Jason Robert's Pluralsight course:

Mocking with Moq 4 and xUnit  
https://app.pluralsight.com/library/courses/mocking-moq-xunit/table-of-contents

To do this, the sample simulates what would be controllers which receive request and returns response DTO objects.

It applies logic and creates entities in a backed in memory dummy db context.

The controller has an external dependency on a service which is used to mock from the unit tests


## Automapper
Used for conversions between request/response DTOs and entity types
