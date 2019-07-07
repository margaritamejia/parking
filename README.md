# Parking Rate

This solution contains the logic and test for in order to get parking rates

## Configuration
The rate configurations are found in the file ratesSettings.json.
Each rate can be modified, with their friendly name, amount to charge and conditions.

Currently there are 3 types of rates:

### Flat Rate
This rate is use when the amount to charge is invariable. 
Can be configured the time(entry and exit), the days (entry and exit) that this fare is applicable and the max days.

### Hourly Rate
This rate is use when the amount to charge increase with the duration in hours. 
Can be configured the max hours that make this rate applicable.

### Default Rate
This rate is use as default.

## Assumptions
No public holidays are considered.
Night Rate => Exit between 3:30 AM to 11:30 AM instead of (PM)