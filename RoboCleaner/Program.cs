using RoboCleaner.Services;

Console.WriteLine("Hello, Dirty House!");

// Sample inputs
string[] input1 = [
    "....o.",
    "...o..",
    ".s...o",
    ".oo...",
    "......",
    ".....o"];

string[] input2 = [
    "...o..",
    "..o...",
    ".....o",
    "o.....",
    ".s..o.",
    ".o...."];

string[] input3 = [
    "...o.",
    "s...o",
    "..o.o" ] ;

// Parse inputs and get Grid2D instances
var inputParserService = new InputParserService();
var grid1 = inputParserService.Parse(input1);
var grid2 = inputParserService.Parse(input2);
var grid3 = inputParserService.Parse(input3);

// Clean floor
Console.WriteLine(new HouseCleaningService(grid1).CleanFloor());
Console.WriteLine(new HouseCleaningService(grid2).CleanFloor());
Console.WriteLine(new HouseCleaningService(grid3).CleanFloor());

Console.ReadKey();
