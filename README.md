You are required to produce a solution that implements the registration of users with different roles within a web interface.
The implementation needs to have a database structure that will support Users, Roles and Actions between the Users [10 marks].
You need to demonstrate the actions each role does via a UI specific to each role. Through the UI the Users should be able to mark any related data as completed [10 marks].
You should have the following roles implemented [10 marks]:
1. Architect
2. Analyst
3. Programmer
4. Tester
5. Manager
Your registration process should give the option to a user to register with one of the aforementioned roles [10 marks].
The user that just registered must be able to login after a Manager has approved his registration [10 marks].
The responsibilities of each role are:
1. The Manager role just accepts or declines new authorizations [10 marks]
2. The Programmer role can view certain data, e.g. files, pages e.tc. [10 marks]
3. The Tester role can view certain data that have been marked from the Programmer as “To Test” [10 marks]
4. The Analyst role provides to the Architect certain data e.g. a document with the analysis of the system [10 marks]
5. The Architect role provides to the Programmer certain data, e.g. a document with the analysis and architecture of the system [10 marks]



Simple Case Scenario

The Manager approves the registration of the users via a specific UI.
The Analyst prepares an analysis for the Architect. The Analyst marks his work as completed and the Architect can see this analysis, e.g. a simple document.
The Architect designs the system’s structure and marks his work as completed.
The Programmer implements the system and marks his work as completed.
The Tester views the implemented system and tests it and marks the testing stage as completed.


