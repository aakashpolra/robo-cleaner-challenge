using RoboCleaner;

Console.WriteLine("Hello, Dirty House!");

string[] input1 = ["....o.", "...o..", ".s...o", ".oo...", "......", ".....o"]; 
string[] input2 = ["...o..", "..o...", ".....o", "o.....", ".s..o.", ".o...."];
string[] input3 = [ "...o.", "s...o", "..o.o" ] ;

Console.WriteLine(new HouseCleaningRobot(input1).CleanFloor());
Console.WriteLine(new HouseCleaningRobot(input2).CleanFloor());
Console.WriteLine(new HouseCleaningRobot(input3).CleanFloor());
Console.ReadKey();
