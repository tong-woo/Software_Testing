# Connect 4

This is the workspace of Group SWT2-2 for the course Software Testing 2022 for VU Amsterdam.

## Testing Setup

1. Run `dotnet restore` and `dotnet tool restore` in the same directory as this README to install Stryker for mutation testing and Moq for Console mocking.
2. Open the solution file using Visual Studio Code (I use VS2022), and open the Test Explorer to examine and run the test suite (Test > Test Explorer).
3. Open a powershell (apparently this does not work on linux, or at least WSL) and navigate to the root of the tests project. Run `dotnet stryker -o` to execute the mutation testing.
   > Mutation tests should take about 1.5 minutes to execute, after which the report should automatically open in the browser.
