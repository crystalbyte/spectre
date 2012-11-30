# The Spectre Framework

The Spectre Framework is an attempt to introduce HTML5 as a first class citizen UI language for CLR based applications.  
Instead of implementing yet another HTML renderer, the framework utilizes an extended version of the Chromium browser to render its markup.
Since the framework is build on top of Chromium, it naturally inherits some of it's traits, such as:

* Multi-Process Architecture
* Great HTML5/CSS3 support
* Full JavaScript support using Google's V8 JS engine
* Native video support using the WebM format
* NPAPI Plugin support
* Native WebGL support
* Great rendering performance

The CLR library exposes many of chromium's internal commands, enabling us to use the browser as a UI rendering system.  
These commands include, but are not limited to:

* Navigation
* IPC messaging
* Resource loading
* Javascript runtime access
* DOM access

Additionally the framework includes a simple integration of the [Razor](http://weblogs.asp.net/scottgu/archive/2010/07/02/introducing-razor.aspx) view engine.

## Building the project

Spectre in itself is fully managed and will therefor build successfully out of the box using VS 2010 or VS 2012 on Windows or Monodevelop on Linux.
However, spectre also depends on several native libraries, which need to be compiled alongside the project in order to make it work.

#### Building the Chromium Embedded Framework

The first thing to do is building the [Chromium Embedded Framework](http://code.google.com/p/chromiumembedded/) which in itself depends on [The Chromium Project](http://www.chromium.org/Home).  
The CEF project exposes the regular chromium libraries and provides a C based access point to it's internal commands.  
Chromium itself is then statically linked into the CEF output assembly (libcef.dll/libcef.so/libcef.dylib).

Detailed build instrucions can be found here: [Building the Chromium Embedded Framework](http://code.google.com/p/chromiumembedded/wiki/BranchesAndBuilding).  
I'd recommend the automated approach, since it is the easiest way.  
Spectre is currently build against the CEF3 trunk in revision 906, so the url to use with the automated tool will look like this:
    http://chromiumembedded.googlecode.com/svn/trunk/cef3@906  
You can try more recent revisions, but binary compatibility cannot be guaranteed, since the API changes regularly.

#### Deploying native dependencies with Spectre

Once CEF is successfully built, we need to copy the dependencies into the spectre based applications output dir.  
All sample projects have already been outfitted with appropriate copy scripts to automate this process.
All that needs to be done is adding an environmental variable to your system with the name 'CHROMIUM_SRC', pointing to the chromium source directory, obviously.

## Running the Samples

If all went well, the platform specific sample applications can be found in the 'samples' directory.  
Currently only samples for the windows platform exist, however, linux support will follow shortly.  
In order to run a specific sample, the project needs to be marked as the startup project and started.
It is important not to enable the Visual Studio Hosting process, since it will screw up Chromium's IPC system, thus crashing the application.






