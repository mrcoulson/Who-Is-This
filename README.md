Who Is This?
=============

This is a cute (debatable) little (indeed) application that searches an Active Directory for a user by username and returns the display name, department, user creation date, and password change date.

Requirements
------------
* ASP.NET
* The System.DirectoryServices namespace (not usually included by default in Visual Studio projects)
* Access to an Active Directory domain controller

Caveats
-------

The AD properties used in this application are ones that we use in our work environment.  For example, your environment may not use "physicalDeliveryOfficeName" for department.  

