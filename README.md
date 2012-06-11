Who Is This?
=============

This is a cute (debatable) little (indeed) application that searches an Active Directory for a user by username and returns a few basic pieces of user info.  Not intended to be a replacement for an AD Users and Computers console, this is to give a technician a quick answer to "Who is this?" when faced with a strange user ID.

Usage
-----

The AD properties used in this application are ones that we use in our work environment.  For example, your environment may not use physicalDeliveryOfficeName for department.  This sort of application could be taken much further in an environment where AD stores user contact information.  In our work environment, this is not the case yet.  Also, the link to the intranet directory may not apply for your organization.

Requirements
------------

* ASP.NET
* The System.DirectoryServices namespace (not usually included by default in Visual Studio projects)
* Access to an Active Directory domain controller

