# MedianEnergy
Filter energy consumptions around median value

Task description

console program that will:
1. Read CSV files, set the file path configurable so the program can read any "LP" and
"TOU" files;
2. For each file, calculate the median value using a) the "Data Value" column for the
"LP" file type or b) or the "Energy" column for the "TOU" file type;
3. Find values that are 20% above or below the median, and print to the console using
the following format:
{file name} {datetime} {value} {median value}
Note: to get {datetime} use "Date/Time" column in a csv file (for both file
types)


Project Structure

MedianEnergy - Console application
It is a DotNet Core console application that reads csv input files from file system and generates output.

Input files path is provided from environment variables. Environment variable is easy to pass in CI/CD and DockerCompose. you can pass different paths for different deployment, without making any change in any application config files.
Code is designed to expect absolute file path like "E:\Work\MedianEnergy\EnergyFiles\TOU"

2 Environment variables:
TOUFiles - E:\Work\MedianEnergy\EnergyFiles\TOU
LPFiles - E:\Work\MedianEnergy\EnergyFiles\LP


MedianEnergy.Engine
Engines process csv files to generate the output.
There is separate service to TOU and LP files, devived from base generic class. 
BaseService generic class provides common functionalies like CalculateMedian, PrintOutput, FilterRecords etc.

Applicaton design allows it to easily expand to support different output formats. As per requirement the output is printed on console but there is another service to write result in an excel file. Type is injected in program.cs via DI.


MedianEnergy.Logger
This project handles logs, traces and info. right now it is only configured to write error logs.
NLog is used to generate logs and write in console, but it can be configured to write in a file as well.

MedianEnergy.UnitTest
Unit tests are created using NUnit testing framework.

Assumption & Limitations
Since it is single page of requirements,following assumptions are considered during the design. The design and implementation may change depending on these assumptions:

1. We expect output records group by file name.
With above assumption, I processed several files in parallel but printing them out in console one at a time.

2. CSV file size is not very big.
Application reads complete csv file in memory, which can be a problem if the file size is very big having millions of records. In that case we may read file with 1 record at a time and store them in SQLLite or database, because we need to calculate median working on complete set.

3. Logs are written in console window

4. LP and TOU files are read from different folders. If we put them in same folder, the application will throw error.
It can be easily fixed, we just need to parse the file name to identify the type. 

5. Value that are 20% above or below median value.
I assumed it means the median value is 100% or threshhold.
example: if median is 20 then 
20% below is 16
20% above is 24

5. CSVHelper Nuget package is used to read csv records in objects.

6. Right now the application only neads Datetime and Value column, but still I am reading all column value.
Code can be changed to only read these two columns instead of everything. It will reduce memory consumption.

7. Decimal precision is upto 6 decimal places
