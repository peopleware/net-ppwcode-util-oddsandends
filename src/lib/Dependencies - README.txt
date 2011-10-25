Dependencies
------------

Outside code on which this solution is dependent is gathered in binary
form in this "lib" directory. Projects in the solution refer to
files in this directory as references. Files are gathered per product
in a subdirectory. Sometimes, such a directory is defined as an svn:external,
so that dependency definitions can be shared between solutions.
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
