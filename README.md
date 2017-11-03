# Band Tracker
##### by Adam Titus

### Description
This is a mockup of a website that allows a user to add bands and venues, and connect them together. Will also allow you to view all the bands playing at a venue, and all the venues a band will be playing at.
### Installation Instructions
* Copy the url for this repository
* Open Bash or something similar
* In Bash, type in `cd desktop` and hit enter
* then type `git clone` plus the url you copied and hit enter
* then type `cd Band-Tracker/BandTracker` and hit enter
* Now type `dotnet restore` and wait for everything to load
* Now open MAMP
* Click the Start Servers button
* Now go back to Bash and type in `MySql -uroot -proot`
* Once your command line changes to `MySql>` type in `CREATE DATABASE band_tracker`
* Now type in the following commands
> * USE band_tracker
> * CREATE TABLE venues(id serial PRIMARY KEY, name VARCHAR(255));
> * CREATE TABLE bands(id serial PRIMARY KEY, name VARCHAR(255));
> * CREATE TABLE bands_venues(band_id INT, venue_id INT);
> * \q
* Now type in dotnet run
* Open to your web browser and navigate too `http://localhost:5000`

### Technology Needed
* MAMP
* MySql
* .Net
* Bash
* Web Browser with Internet

### Specs
|Behavior|Input|Output|
|-|-|-|
|Will allow user to add venue to database|Madison Square Gardens|Madison Square Gardens|
|Will allow user to add band to database|Twenty One Pilots|Twenty One Pilots|
|Will allow user to link band to venue and vice versa|Madison Square Gardens - Twenty One Pilots| Madison Square Gardens - Twenty One Pilots|
|Will allow user to update venue name| Madison Square Gardens of Glory|Madison Square Gardens of Glory - Twenty One Pilots|
|Will allow user to delete venue| Madison Square Gardens|Twenty One Pilots|

### Known Bugs
* Cannot delete Bands currently

### Contact Me
You can reach me at adamtitus76@gmail.com or connect with me on [linkedin](www.linkedin.com/in/adam-titus-06740b149).
#### Legal
This is licensed under the MIT license

Copyright (c) 2017 Twenty One Pilots Titus All Rights Reserved.

_If you find a way to monetize this please contact the author_
