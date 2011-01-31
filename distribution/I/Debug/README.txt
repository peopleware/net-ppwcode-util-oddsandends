Dependencies
------------

This directory is intended to be used as svn:external in .net projects.
It contains the definition of a dependency on an external product.

These dependencies are needed for compilation and build, and for running
the code. When you use (parts of) this solutions in other products,
you need the artifacts this solution depends on, recursively, also in
the other product.

When possible, a copy of the required files is kept in this directory
in the repository, so that developers can get started easily. However,
for some dependencies, license issues prohibit us to distribute the
dependencies ourselfs. The developer needs to retrieve them from the
original source himself. In this case, this document describes how to
get the required files.


Microsoft Contracts
-------------------

Version: Release 1.4.31130.0 (November 30, 2010)
(see <http://research.microsoft.com/en-us/projects/contracts/releasenotes.aspx>)

To get the required DLL:
* Download the distribution from
  <http://msdn.microsoft.com/en-us/devlabs/dd491992>.
  We use the "Standard Edition".
  The downloaded file is "Contracts.devlab9std.msi".
* Run the installer (this also installs extra components in Visual Studio)
* Copy the file "Microsoft.Contracts.dll" from
  "C:\Program Files\Microsoft\Contracts\PublicAssemblies\v3.5\"
  to this directory.


Spring Core
-----------

Version: Spring.NET 1.3.1, for .net 2.0

The files are from Spring.NET 1.3.1, downloaded from
<http://www.springframework.net/download.html>,
in [bin\net\2.0\].
Since we are still on .net 3.5, it is the 2.0 files we need to use.

These files are released under the Apache License v2.
