# Hospital Priority Queue System

just a simple hospital patient management system i built for my data structures class. patients get prioritized by severity level (1-10) and if they have the same severity, whoever came first gets seen first (FIFO).

## what it does

- add patients with their name and severity
- automatically sorts them in the queue by priority
- serve the next patient (takes first in line)
- search for specific patients by ID super fast

## how to use

basically just run the backend with `dotnet run` and then open index.html in your browser. make sure the backend is running on port 5199 or change the API_URL in script.js

### pages
- **home** - landing page with info about the system
- **manage** - where you add patients and see the current queue
- **search** - lookup any patient by their ID

## tech stuff

built with ASP.NET core for backend and vanilla js/html/css for frontend. using linkedlist for the queue and dictionary for fast lookups. tried to keep it simple and clean

the design is kinda inspired by one medical's website bc i liked their color scheme

## notes

might add more features later like editing patient info or showing wait times. for now it works fine for what i need
