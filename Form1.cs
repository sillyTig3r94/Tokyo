#define DISPLAY
//#define MSG
#define DEF_LIST
#define DEF_VAR
#define DEF_OBJ
//#define MSG_TOOLNAME
#define DEF_SIZE_1
//define DEF_SIZE_2
//#define DEF_SIZE_3
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BHA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
#if (DISPLAY)
            groupBox_Debug.Visible = true;
#else
            groupBox_Debug.Visible = false;   
#endif
        }

#if DEF_VAR
        /*
         * Variable
         */
        public int numofTool = 0;

        public char[] delimiter_for_dataStructure = { ' ', ',', '.', ':', '\t' };

        public char[] delimiter_for_property = { ' ' };

        public int numTool = 0;

        bool isDragged = false;

        string myPath = Directory.GetCurrentDirectory();
#endif

#if  DEF_OBJ
        /* 
         * Object 
         */

        /* my node - size and location relative to my win form */
        static Point DEF_NodeLoc = new Point(50, 30);

#if DEF_SIZE_1
        static Size myNode_Size = new Size(120, 60);
#endif

#if DEF_SIZE_2
        static Size myNode_Size = new Size(180, 100);
#endif
        static Point myNode_Loc = new Point(DEF_NodeLoc.X, DEF_NodeLoc.Y);

        /* my module size and location relative to my win form */
        static Point DEF_ModuleLoc = new Point(50, 230);

#if DEF_SIZE_1
        static Size myModule_Size = new Size(90, 30);
#endif

#if DEF_SIZE_2
        static Size myModule_Size = new Size(120, 75);
#endif

        static Point myModule_Loc = new Point(DEF_ModuleLoc.X, DEF_ModuleLoc.Y);

        public Point ptOffset;

        public Point nextNode_Loc;

        public Point drag_Loc;
#endif

#if DEF_LIST
        /*
         * this is my list object for this project
         */
        List<GroupBox> myNodeList = new List<GroupBox>();

        List<Button> myModuleList = new List<Button>();

        List<bool> myAvailableNode = new List<bool>();

        List<int> myModuleLoc = new List<int>();

        List<moduleObject> myToolString = new List<moduleObject>();

        List<string> tool_data = new List<string>();

        List<int> id_list = new List<int>();

        List<string> name_list = new List<string>();

        List<int> opvoltage_list = new List<int>();

        List<int> maxvoltage_list = new List<int>();

        List<int> minvoltage_list = new List<int>();

        List<int> resistance_list = new List<int>();

        List<int> power_list = new List<int>();

        List<int> length_list = new List<int>();

        List<bool> sink_list = new List<bool>();

        List<bool> source_list = new List<bool>();
#endif

        /*
         * this my tool object 
         */
        public class moduleObject
        {
            public int id { get; set; }
            public string toolName { get; set; }
            public int opVolt { get; set; }
            public int maxVolt { get; set; }
            public int minVolt { get; set; }
            public int power { get; set; }
            public int res { get; set; }
            public int length { get; set; }
            public bool sink { get; set; }
            public bool source { get; set; }

            public moduleObject(int id, string name, int OpVolt, int MaxVolt, int MinVolt, int Watt, int Res, int len, bool Sink, bool Source)
            {
                this.id = id;

                this.toolName = name;

                this.opVolt = OpVolt;

                this.maxVolt = MaxVolt;

                this.minVolt = MinVolt;

                this.power = Watt;

                this.res = Res;

                this.length = len;

                this.sink = Source;

                this.source = Sink;
            }
        }

        /* Initiliase my form here */
        private void Form1_Load(object sender, EventArgs e)
        {
            /* 
             * read data information in the text file first  
             */
            StreamReader datafile = new StreamReader(myPath + @"\ToolParams.csv"); //should use relative path here

            string line = String.Empty;

            while ((line = datafile.ReadLine()) != null)
            {

                tool_data.Add(line);
                // MessageBox.Show(line);
                numofTool++;
            }
            /*
             * parsing properties of the tools 
             */
            for (int index = 1; index < tool_data.Count(); index++) //ignore the first line
            {
                // MessageBox.Show(tool_data[index]);

                string[] words = tool_data[index].Split(delimiter_for_dataStructure);
                /*
                 * Assign my shitty list here
                 */
                id_list.Add(Convert.ToInt32(words[0]));
                name_list.Add(words[1]);
                opvoltage_list.Add(Convert.ToInt32(words[2]));
                maxvoltage_list.Add(Convert.ToInt32(words[3]));
                minvoltage_list.Add(Convert.ToInt32(words[4]));
                power_list.Add(Convert.ToInt32(words[5]));
                resistance_list.Add(Convert.ToInt32(words[6]));
                length_list.Add(Convert.ToInt32(words[7]));
                sink_list.Add(Convert.ToBoolean(words[8]));
                source_list.Add(Convert.ToBoolean(words[9]));
            }

            /* Create the list of available tool module */
            for (int numModule = 0; numModule < name_list.Count; numModule++)
            {
                string tool_name = String.Empty;
                /* update location for next node */
                myModule_Loc.X = DEF_ModuleLoc.X + numModule * (myModule_Size.Width + myModule_Size.Width / 10);
                Point nextModule_Loc = myModule_Loc;
                Button module_drag = new Button();
                /* add properties */
                module_drag.Size = myModule_Size;
                module_drag.Location = nextModule_Loc; //default location
                module_drag.Name = "module_" + name_list[numModule];
                tool_name = name_list[numModule];

#if MSG_TOOLNAME
                MessageBox.Show(tool_name);
#endif
                /* adding icon for my tool */
                try
                {
                    switch (tool_name)
                    {
                        case "GeoPilot":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_geopilot.png");
                            module_drag.Text = String.Empty;
                            break;
                        case "RSS":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_rss.png");
                            module_drag.Text = String.Empty;
                            break;
                        case "Directional":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_directional.png");
                            module_drag.Text = String.Empty;
                            break;
                        case "Gamma":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_gamma.png");
                            module_drag.Text = String.Empty;
                            break;
                        case "EarthStarTx":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_earthstartTx.png");
                            break;
                        case "EarthStarRx":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_earthstartRx.png");
                            break;
                        case "Generator":
                            module_drag.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\tool_generator.png");
                            break;
                        default:
                            module_drag.Text = name_list[numModule];
                            break;
                    }

                    module_drag.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    // MessageBox.Show("No icon found for " + tool_name);
                    module_drag.Text = name_list[numModule];
                }
                module_drag.Tag = (numModule + 1).ToString();
                /* add drag-drop event */
                module_drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseDown);
                module_drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseMove);
                module_drag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseUp);
                module_drag.SendToBack();
                /* add new module to my form */
                //this.Controls.Add(module_drag);
                this.Controls.Add(module_drag);
                myModuleList.Add(module_drag);
            }

            /*
             * Adding picture for my remove and add button 
             */

            try
            {
                bt_removeNode.Text = String.Empty;
                bt_removeNode.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\button_remove.png");
                bt_removeNode.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                /* in case the icon is not found, put name for my button*/
                bt_removeNode.Text = "Remove Node";
            }

            try
            {
                bt_addNode.Text = String.Empty;
                bt_addNode.BackgroundImage = Image.FromFile(myPath + @"\Tool Icon\button_add.png");
                bt_addNode.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                /* in case the icon is not found, put name for my button*/
                bt_addNode.Text = "Add Node";
            }

            datafile.Close();   //close file after read, don't ask why (-_-)

            /*
             * Re-size my form here
             */

            this.Size = new Size(myModuleList.Count() * (myModule_Size.Width + 2 * myModule_Size.Width / 10), this.Height);
#if DISPLAY
            this.Size = new Size(this.Width, this.Height + groupBox_Debug.Height + 50);
#endif
        }

        /* Button click event to add new node to my form */
        private void bt_addNode_Click(object sender, EventArgs e)
        {
            /*
             * add a node when button is pressed
             * number node added cannot more than number of
             * available tool
             */
            if (numTool < name_list.Count)    //check only add less than available tool
            {
                /* Create Node Framework */
                nextNode_Loc = myNode_Loc;
                GroupBox node_frame = new GroupBox();
                /* initialize node properties */
                node_frame.Size = myNode_Size;
                node_frame.Location = nextNode_Loc; //default location                                       
                node_frame.Name = "node_frame_" + (numTool + 1).ToString();
                node_frame.Text = "Node : " + (numTool + 1).ToString();
                node_frame.Tag = (numTool + 1).ToString();
                node_frame.SendToBack();
                /* add node frame to my form */
                this.Controls.Add(node_frame);
                myNodeList.Add(node_frame);
                /* increase index and update location for next node */
                numTool++;
                myNode_Loc.X = DEF_NodeLoc.X + numTool * (myNode_Size.Width + myNode_Size.Width / 10); //update location for next node
                /* Create my avaiable Node */
                myAvailableNode.Add(false); //default 
                myModuleLoc.Add(0); // default
            }

            /*
             * if the we have more node in our list then enable our 
             * remove button
             */
            if (myNodeList.Count != 0)
                bt_removeNode.Enabled = true;
            else
                bt_removeNode.Enabled = false;

            /*
             * update confirm button status 
             */
            foreach (var item in myAvailableNode)
            {
                if (item == false)
                {
                    bt_confirmNode.Enabled = false;
                    bt_runPython.Enabled = false;
                    break;
                }
                else
                {
                    bt_confirmNode.Enabled = true;
                }
            }

            /*
             * Resize my Form when add node is pressed 
             */
            var size_width = myNodeList.Count() * (myNode_Size.Width + myNode_Size.Width / 10) + 2 * DEF_NodeLoc.X;
            if(size_width > this.Width)
                this.Size = new Size(size_width, this.Height);

        }

        /* Button click event to remove new node to my form */
        private void bt_removeNode_Click(object sender, EventArgs e)
        {
            /*
             * remove the node out of my form
             */
            this.Controls.Remove(myNodeList[myNodeList.Count - 1]);

            /*
             * remove the node out of my list of object
             */
            myNodeList.RemoveAt(myNodeList.Count - 1);
            myAvailableNode.RemoveAt(myAvailableNode.Count - 1);
            myModuleLoc.RemoveAt(myModuleLoc.Count - 1);

            /* 
             * increase index and update location for next node 
             */
            numTool--;

            /*
             * update my node location again ? Why ? Ask yourself ?
             */
            myNode_Loc.X = DEF_NodeLoc.X + numTool * (myNode_Size.Width + myNode_Size.Width / 10);

            /*
             * disable if the node is back to zero, otherwise keep it active
             */
            if (myNodeList.Count == 0)
                bt_removeNode.Enabled = false;
            else
                bt_removeNode.Enabled = true;

            /*
             * update confirm button status 
             */
            foreach (var item in myAvailableNode)
            {
                if (item == false)
                {
                    bt_confirmNode.Enabled = false;
                    bt_runPython.Enabled = false;
                    break;
                }
                else
                {
                    bt_confirmNode.Enabled = true;
                }

            }

            /*
             * Resize my form when remove node is pressed
             */
            var size_width = myNodeList.Count() * (myNode_Size.Width + myNode_Size.Width / 10) + 2 * DEF_NodeLoc.X;
            if (size_width > (myModuleList.Count * (myModule_Size.Width + myModule_Size.Width / 10) + DEF_ModuleLoc.X))
                this.Size = new Size(this.Width - (myNode_Size.Width + myNode_Size.Width / 10), this.Height);
        }

        /* Button click event to confirm the tool selection on each node */
        private void bt_confirmNode_Click(object sender, EventArgs e)
        {
            /*
             * remove all my tool everytime user confirm new setup
             */
            myToolString.Clear();   // just in case

            /*
             * loop to add properties for my tool
             */
            foreach (var ToolId in myModuleLoc)
            {
                moduleObject module = new moduleObject(
                id_list[ToolId - 1],
                name_list[ToolId - 1],
                opvoltage_list[ToolId - 1],
                maxvoltage_list[ToolId - 1],
                minvoltage_list[ToolId - 1],
                power_list[ToolId - 1],
                resistance_list[ToolId - 1],
                length_list[ToolId - 1],
                source_list[ToolId - 1],
                sink_list[ToolId - 1]);
                myToolString.Add(module);
            }

            /*
             * only for debug, nothing special
             */
            debug_toolName.Text = String.Empty;
            debug_toolVoltage.Text = String.Empty;

            /*
             * only for debug, nothing special
             */
            foreach (var newtool in myToolString)
            {
                debug_toolName.Text += " " + newtool.toolName;
                debug_toolVoltage.Text += " " + newtool.opVolt;
            }

            /*
             * Enable my Calculation Tab 
             */

            bt_runPython.Enabled = true;

            //

            /* 
             * Create new Form when all the tool is setup/confirm
             */
            //var myCalculationForm = new Form();

            /*
             * Add load event for my calculation form
             */
            //myCalculationForm.Load += new System.EventHandler(ProgramViwer_Load);
            //myCalculationForm.Show();
        }

        private void bt_runPython_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("python_script.bat");

            string python_path = myPath + @"\PyLTSpice"; // current directory + sub folder

            string root_path = Path.GetPathRoot(Environment.CurrentDirectory);

            /*
             * Create text file that contains information of bha setup, that python create 
             * will use/read to execute
             */
            File.WriteAllText(python_path + @"\tools_setup.txt", string.Empty);
            foreach (var newtool in myToolString)
            {
                File.AppendAllText(python_path + @"\tools_setup.txt",newtool.toolName + Environment.NewLine);
            }


            /* 
             * Create batch file that run my python script
             */
            sw.WriteLine(root_path);
            sw.WriteLine("cd " + python_path);
            sw.WriteLine(@"python ltspice.py");
            sw.Close();

            Process python_script = null;
            try
            {

                string batDir = myPath;
                python_script = new Process();
                python_script.StartInfo.WorkingDirectory = batDir;
                python_script.StartInfo.FileName = "python_script.bat";
                python_script.StartInfo.CreateNoWindow = true;
                python_script.Start();
                python_script.WaitForExit();
                // MessageBox.Show("Bat file executed !!");

                read_script(python_path, "uphole_result.txt",true);

                read_script(python_path, "downhole_result.txt",false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void read_script(string path,string str_file,bool downhole)
        {
            /* 
             * read data information in the text file first  
             */
            StreamReader datafile = new StreamReader(path + @"\" + str_file); //should use relative path here

            string line = String.Empty;

            while ((line = datafile.ReadLine()) != null)
            {
                if(downhole)
                    txb_DownHole_Read.AppendText(line + Environment.NewLine);
                else
                    txb_Uphole_Read.AppendText(line + Environment.NewLine);
            }
        }

        /* tool drag - drop event , when left mouse down */
        private void module_drag_MouseDown(object sender, MouseEventArgs e)
        {
            /*
             * Make my life easier, don't ask why ?
             */
            Button module_sel = sender as Button;

            /*
             * check if mouse click is left mouse,
             * are you going to drag this tool by right mouse ?
             * Really ?
             */
            if (e.Button == MouseButtons.Left)
            {
                isDragged = true;
                drag_Loc = module_sel.Location;
                Point ptStartPosition = module_sel.PointToScreen(new Point(e.X, e.Y));
                ptOffset = new Point();
                ptOffset.X = module_sel.Location.X - ptStartPosition.X;
                ptOffset.Y = module_sel.Location.Y - ptStartPosition.Y;
            }
            else
            {
                isDragged = false;  // Is it not obvious why ?
            }
        }

        /* tool drag - drop event , when dragging */
        private void module_drag_MouseMove(object sender, MouseEventArgs e)
        {
            /*
             * Make my life easier, don't ask why ?
             */
            Button module_sel = sender as Button;

            /*
             * this is where magic happen, relocate the 
             * tool according to the location of the mouse
             */
            if (isDragged)
            {
                Point newPoint = module_sel.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(ptOffset);
                module_sel.Location = newPoint;
            }
        }

        /*
         * tool drag - drop event, when left mouse is released
         * Also, this event re-scan the position of the tool on 
         * all the node to update available node on my tool string
         */
        private void module_drag_MouseUp(object sender, MouseEventArgs e)
        {
            /*
             * No comment
             */
            Button module_sel = sender as Button;

            isDragged = false;

            BHA_swappingTool(module_sel);

            BHA_scanTool(module_sel);


        }

        private void ProgramViwer_Load(object sender, System.EventArgs e)
        {
            //MessageBox.Show("New form is released");
        }

        private void BHA_scanTool(object sender)
        {
            Button module_sel = sender as Button;

            bool node_occupied = false;

            int node_index = 0;

            /* 
             * re-scan all my node everytime,
             * user move the tool around
             */

            foreach (var node in myNodeList)
            {

                foreach (var module in myModuleList)
                {
                    // MessageBox.Show("X: " + item.Location.X.ToString() + "Y: " + item.Location.Y.ToString());
                    if (module.Location.X > node.Location.X
                        && module.Location.X < (node.Location.X + myNode_Size.Width)
                        && module.Location.Y > node.Location.Y
                        && module.Location.Y < (node.Location.Y + myNode_Size.Height))
                    {
                        debug_avaiableNode.Text = String.Empty;
                        debug_NodeID.Text = String.Empty;

                        /* 
                         * Re-location myModule in myNode , why ?
                         * because user love to see the clipping animation
                         */
                        int offset_x = (myNode_Size.Width - myModule_Size.Width) / 2;
                        int offset_y = (myNode_Size.Height - myModule_Size.Height) / 2;
                        Point module_newLoc = new Point(node.Location.X + offset_x, node.Location.Y + offset_y);
                        module.Location = module_newLoc;

#if MSG
MessageBox.Show("Offset x: " + offset_x.ToString() + "Offset y: " + offset_y.ToString());
MessageBox.Show("Bounderies x = " + (node.Location.X + myNode_Size.Width).ToString() + "y = " + (node.Location.Y + myNode_Size.Height).ToString());
#endif


                        /* 
                         * update my available node list 
                         */
                        myAvailableNode[node_index] = true;
                        myModuleLoc[node_index] = Convert.ToInt32(module.Tag);

                        /* 
                         * Update the node status 
                         */
                         if(module.Text == "Brussels")
                            node.BackColor = Color.Red;
                         else
                            node.BackColor = Color.LightBlue;

                        /*
                         * only for debug, display out my form
                         */
                        foreach (var status in myAvailableNode)
                            debug_avaiableNode.Text += " " + status.ToString();

                        foreach (var id in myModuleLoc)
                            debug_NodeID.Text += " " + id.ToString();

                        /*
                         * set this if my node is occupied
                         */
                        node_occupied = true; //notify node is occupied

                        break;
                    }
                    else
                    {
                        continue;     // if not found tool in the node then just skip to next tool              
                    }

                }

                /*
                 * if my node is occupied then i have to
                 * update my node status
                 */
                if (!node_occupied)
                {
                    myAvailableNode[node_index] = false;
                    myModuleLoc[node_index] = 0;
                    node.BackColor = SystemColors.Control;

                    debug_avaiableNode.Text = String.Empty;
                    debug_NodeID.Text = String.Empty;

                    foreach (var status in myAvailableNode)
                        debug_avaiableNode.Text += " " + status.ToString();

                    foreach (var id in myModuleLoc)
                        debug_NodeID.Text += " " + id.ToString();
                }


                node_index++;   // increase index for next node 

                node_occupied = false;  // 
            }

            /* 
             * update new confirm button
             */
            foreach (var item in myAvailableNode)
            {
                if (item == false)
                {
                    bt_confirmNode.Enabled = false;
                    break;
                }
                else
                {
                    bt_confirmNode.Enabled = true;
                }

            }
        }

        private void BHA_swappingTool(object sender)
        {
            Button module_sel = sender as Button;

            //bool node_occupied = false;

            //int node_index = 0;

            /*
             * Swapping module
             */
            int myId = 0;
            foreach (var node in myNodeList)
            {
                if (myAvailableNode[myId] == true
                        && module_sel.Location.X > node.Location.X
                        && module_sel.Location.X < (node.Location.X + myNode_Size.Width)
                        && module_sel.Location.Y > node.Location.Y
                        && module_sel.Location.Y < (node.Location.Y + myNode_Size.Height))

                {

                    /* 
                    * First re-location myModule in myNode , why ?
                    * because user to see the sticking animation
                    */

                    // MessageBox.Show("Swapping");

                    int offset_x = (myNode_Size.Width - myModule_Size.Width) / 2;
                    int offset_y = (myNode_Size.Height - myModule_Size.Height) / 2;
                    Point module_newLoc = new Point(node.Location.X + offset_x, node.Location.Y + offset_y);
                    module_sel.Location = module_newLoc;

                    myModuleList[myModuleLoc[myId] - 1].Location = drag_Loc;


                    break;
                }
                myId++;
            }
        }


    }
}
