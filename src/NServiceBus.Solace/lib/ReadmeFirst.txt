This directory contains the following Solace Systems Messaging APIs assemblies:

lib/SolaceSystems.Solclient.Messaging.dll
                * The platform target for this assembly is x86
                * At runtime, it requires libsolclient.dll (x86 native dll) to be in the path
                * It has the same strong name as lib/64/SolaceSystems.Solclient.Messaging.dll

lib/64/SolaceSystems.Solclient.Messaging.dll
                * The platform target for this assembly is x64
                * At runtime, it requires libsolclient_64.dll (x64 native dll) to be in the path
                * It has the same strong name as lib/SolaceSystems.Solclient.Messaging.dll
                
lib/SolaceSystems.Solclient.Messaging_64.dll
                * The platform target for this assembly is x64
                * At runtime, it requires libsolclient_64.dll (x64 native dll) to be in the path
                * It has a different strong name than the rest of the bundled assemblies
                * When compiling/building against this assembly:
                                * The Target Platform of the executable must be x64, in this case
                                  the executable will only run on a 64-bit machine
                                * Setting the executable's Target Platform to Any CPU will
                                  produce an executable that can only run on 64 bit machines

Note:
It is possible to build one executable that is able to run as a 32-bit process on a 32-bit machine 
and as a 64-bit process on a 64-bit machine (without WOW64). Here's how:
- Select Any CPU as a Target Platform for the executable
- Reference lib/64/SolaceSystems.Solclient.Messaging.dll or lib/SolaceSystems.Solclient.Messaging.dll  
- When deploying the executable on a 32-bit machine, make sure to bundle with it
  lib/SolaceSystems.Solclient.Messaging.dll + lib/libsolclient.dll
- When deploying the executable on a 64-bit machine, make sure to bundle with it
  lib/64/SolaceSystems.Solclient.Messaging.dll + lib/libsolclient_64.dll

This does not imply that you can simply switch the 64 bit native library for the 32 bit version on
a 64 bit machine and expect it to work. Rather it means you need to switch the DLLs when changing
architectures, rather than recompile the whole program.

To use SSL connections, the openSSL DDLs will have to be bundled in the same directory as SolaceSystems.Solclient.Messaging.dll
The openSSL DLLs are located :
32-bits : lib\3rdparty\32
64-bits : lib\3rdparty\64


Copyright 2009-2015 Solace Systems, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to use and copy the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
UNLESS STATED ELSEWHERE BETWEEN YOU AND SOLACE SYSTEMS, INC., THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
