//Declaring Session Variables
using System.Threading.Tasks;

string task = "0";
int taskcount = 1;
string[] tasklist = new string[taskcount];
int i;

for(i=0; i<taskcount; i++)
    tasklist[i] = "i+1";

string menuitem = "0";


Console.WriteLine("Welcome to Task Manager 2");
 Console.WriteLine("Please Create Entire Task With Detail Including Due Date");
// Input Task Item
task = Console.ReadLine();
while (true)
{
 Console.WriteLine("Menu: Press 1 To Open All Task");
    Console.WriteLine("Menu: Press 2 To Create Task");

    menuitem = Console.ReadLine();
    if (menuitem == "1")
    {
        Console.WriteLine(task);
    }
    if (menuitem == "2")
    {
        Console.WriteLine(task);
    }

}

