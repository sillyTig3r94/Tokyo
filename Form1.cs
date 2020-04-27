//#define DISPLAY
//#define MSG
#define DEF_LIST
#define DEF_VAR
#define DEF_OBJ
//#define SET_ICON
//#define MSG_Name
#define DEF_SIZE_1
//#define DEF_SIZE_2
//#define DEF_SIZE_3
//#define DEF_LOC1
#define DEF_LOC2
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public int numofTool = 0;

        public char[] delimiter_for_dataStructure = { ',', ':', '\t' };

        public char[] delimiter_for_result = { ' ', ',', '\t' };

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
        static Size myNode_Size = new Size(160, 125);
#endif

#if DEF_SIZE_2
        static Size myNode_Size = new Size(180, 100);
#endif
        static Point myNode_Loc = new Point(DEF_NodeLoc.X, DEF_NodeLoc.Y);

        /* my module size and location relative to my win form */
#if DEF_LOC1
    static Point DEF_ModuleLoc = new Point(50, 230);
#endif

#if DEF_LOC2
    static Point DEF_ModuleLoc = new Point(50, 500);
#endif


#if DEF_SIZE_1
        static Size myModule_Size = new Size(120, 110);
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
         * this is my list of object for this project
         */
        List<GroupBox> Nodes = new List<GroupBox>();

        List<GroupBox> Modules = new List<GroupBox>();

        List<Label> lb_results = new List<Label>();

        List<bool> availableNodes = new List<bool>();

        List<int> modLocations = new List<int>();

        List<Tool_Properties> Tools = new List<Tool_Properties>();

        List<Tool_Properties> toolConfigs = new List<Tool_Properties>();

        List<string> tools_result = new List<string>();

        List<string> tool_data = new List<string>();


#endif
        /* 
         * Json properties  
         */

        List<string> nameOf_Tool = new List<string>(); // name of each tool
        JToken database;
        /*
         * this my tool object 
         */
        public class Tool_Properties
        {
            public int id { get; set; }
            public string Name { get; set; }
            public double Power { get; set; }
            public string Load { get; set; }
            public bool Enable { get; set; }
            public bool Sink { get; set; }
            public string Source { get; set; }

            public string BatteryHealth { get; set; }

            public Tool_Properties(
                int id,
                string name,
                double Watt,
                string pwr_option,
                bool enable,
                bool Sink,
                string Source,
                string BatteryHealth)
            {
                this.id = id;

                this.Name = name;

                this.Power = Watt;

                this.Load = pwr_option;

                this.Enable = enable;

                this.Sink = Sink;

                this.Source = Source;

                this.BatteryHealth = BatteryHealth;

            }
        }

        /* Initiliase my form here */
        private void Form1_Load(object sender, EventArgs e)
        {
            //List<Tool> items;
            //* extra Json file */
            string json = String.Empty;

            string python_path = myPath + @"\PyLTSpice"; // current directory + sub folder

            try
            {
                using (StreamReader json_reader = new StreamReader(python_path + @"\" + "database.json"))
                    json = json_reader.ReadToEnd();                
            }
            catch (Exception a)
            { 
                MessageBox.Show(a.Message);
            }

             database = JToken.Parse(json);

            /* iterate from all the json file */
            foreach(JProperty x in database)
            {
                /* iterate through all the tool and get name of the tool */
                string t_name = x.Name;
                JToken ts_attr = x.Value;
                nameOf_Tool.Add(x.Name);
                foreach (JProperty y in ts_attr)
                {
                    /* iterate through all attribute of each tool */
                    string attr_name = y.Name;
                    JToken value = y.Value;
                    // MessageBox.Show(value.ToString());
                }
            }

            foreach(var name in nameOf_Tool)
            {
                // MessageBox.Show(name);
                int tool_id = Convert.ToInt32(database[name]["id"]);
                string tool_name = Convert.ToString(name);
                double pwr = 1.0;
                string pwr_option = "average"; //default = "average"
                bool enable = true; //default = true
                JObject obj = (JObject)database[name];
                bool sink; string source;
                if (obj.ContainsKey("Load"))
                {
                    //MessageBox.Show(name + " is load");
                    sink = true;
                }
                else
                {
                    //MessageBox.Show(name + " is not load");
                    sink = false;
                }

                if (obj.ContainsKey("Source"))
                {
                    //MessageBox.Show(name + " is source");
                    if (database[name]["Source"]["Type"].ToString() == "Generator")
                        source = "Gen";
                    else if (database[name]["Source"]["Type"].ToString() == "Battery")
                        source = "Batt";
                    else
                        source = "FALSE";

                }
                else
                {
                    //MessageBox.Show(name + " is not source");
                    source = "FALSE";
                }

                string health = "Vcharged";
                Tool_Properties tool = new Tool_Properties(tool_id, tool_name, pwr, pwr_option, enable, sink, source,health);

                toolConfigs.Add(tool);
            }

            //StreamReader datafile = new StreamReader(myPath + @"\Tool_Available.txt"); //should use relative path here

            //string line = String.Empty;

            //while ((line = datafile.ReadLine()) != null)
            //{
            //    tool_data.Add(line);
            //    numofTool++;
            //}
            /*
             * parsing properties of the tools 
             */
            //for (int index = 1; index < numofTool; index++) //ignore the first line
            //{

            //    string[] words = tool_data[index].Split(delimiter_for_dataStructure);
            //    /*
            //     * Assign my shitty list here
            //     */
            //    int tool_id = Convert.ToInt32(words[0]);
            //    string tool_name = Convert.ToString(words[1]);
            //    double pwr = Convert.ToDouble(words[2]);
            //    string pwr_option = "average"; //default = "average"
            //    bool enable = Convert.ToBoolean(words[4]);
            //    bool sink = Convert.ToBoolean(words[5]);
            //    string source = Convert.ToString(words[6]);

            //    Tool_Properties tool = new Tool_Properties(tool_id, tool_name, pwr, pwr_option, enable, sink, source);
            //    toolConfigs.Add(tool);
            //}



            // MessageBox.Show(toolConfigs.Count().ToString());

            // MessageBox.Show(numOf_tool.ToString());

            /* Create the list of available tool module */
            for (int numModule = 0; numModule < toolConfigs.Count(); numModule++)
            {
                string tool_name = String.Empty;
                /* update location for next node */
                myModule_Loc.X = DEF_ModuleLoc.X + numModule * (myModule_Size.Width + myModule_Size.Width / 10);
                Point nextModule_Loc = myModule_Loc;
                GroupBox module_drag = new GroupBox();
                /* add properties */
                module_drag.Size = myModule_Size;
                module_drag.Location = nextModule_Loc; //default location
                module_drag.Name = "module_" + toolConfigs[numModule].Name; // replace with json name list
                module_drag.BackColor = Color.LightGray;
                tool_name = toolConfigs[numModule].Name;
                //module_drag.Text = toolConfigs[numModule].Name; // replace with json name list
                module_drag.Text = toolConfigs[numModule].Name;
                module_drag.Tag = (numModule + 1).ToString();
                /* add drag-drop event */
                module_drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseDown);
                module_drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseMove);
                module_drag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.module_drag_MouseUp);
                module_drag.SendToBack();
                /* add new module to my form */
                if (toolConfigs[numModule].Source == "Batt")
                {
                    /* this is for enable / disable */
                    CheckBox chb_enable = new CheckBox();
                    chb_enable.Text = "Enable";
                    chb_enable.Name = "src_enable_" + toolConfigs[numModule].Name;
                    chb_enable.Location = new Point(5, 20);
                    chb_enable.Size = new Size(75, 21);
                    chb_enable.Tag = module_drag.Tag;
                    chb_enable.Checked = true;
                    chb_enable.CheckedChanged += new System.EventHandler(this.enable_option_change);
                    module_drag.Controls.Add(chb_enable);
                    
                    /* this is for enable / disable */
                    Label lb_type = new Label();
                    lb_type.Text = "Battery";
                    lb_type.Name = "battery_" + toolConfigs[numModule].Name;
                    lb_type.Location = new Point(5, 50);
                    lb_type.Size = new Size(130, 20);
                    module_drag.Controls.Add(lb_type);

                    /* this is for voltage delepted/ vcharged */
                    ComboBox cmb = new ComboBox();
                    cmb.Name = "src_voltage_" + toolConfigs[numModule].Name;
                    cmb.Items.Add("Vcharged");
                    cmb.Items.Add("Vdepleted");
                    cmb.Text = "Vcharged";
                    cmb.Location = new Point(5, 80);
                    cmb.Size = new Size(100, 24);
                    cmb.Tag = module_drag.Tag;
                    module_drag.Controls.Add(cmb);
                    cmb.SelectedIndexChanged += new System.EventHandler(this.battery_health_change);
                }

                /* add new module to my form */
                if (toolConfigs[numModule].Source == "Gen")
                {
                    /* this is for enable / disable */
                    CheckBox chb_enable = new CheckBox();
                    chb_enable.Text = "Enable";
                    chb_enable.Name = "src_enable_" + toolConfigs[numModule].Name;
                    chb_enable.Location = new Point(5, 20);
                    chb_enable.Size = new Size(75, 21);
                    chb_enable.Tag = module_drag.Tag;
                    chb_enable.Checked = true;
                    chb_enable.CheckedChanged += new System.EventHandler(this.enable_option_change);
                    module_drag.Controls.Add(chb_enable);
                    

                    /* this is for enable / disable */
                    Label lb_type = new Label();
                    lb_type.Text = "Generator";
                    lb_type.Name = "generator_" + toolConfigs[numModule].Name;
                    lb_type.Location = new Point(5, 50);
                    lb_type.Size = new Size(130, 20);
                    module_drag.Controls.Add(lb_type);
                }


                if (toolConfigs[numModule].Sink == true)
                {
                    /* this is for enable / disable */
                    CheckBox chb_enable = new CheckBox();
                    chb_enable.Text = "Enable";
                    chb_enable.Name = "sink_enable_" + toolConfigs[numModule].Name;
                    chb_enable.Location = new Point(5, 20);
                    chb_enable.Size = new Size(75, 21);
                    chb_enable.Tag = module_drag.Tag;
                    chb_enable.Checked = true;
                    chb_enable.CheckedChanged += new System.EventHandler(this.enable_option_change);
                    module_drag.Controls.Add(chb_enable);

                    /* this is sink average/peak power calculation option */
                    ComboBox cmb = new ComboBox();
                    cmb.Name = "sink_" + toolConfigs[numModule].Name;
                    cmb.Items.Add("average");
                    cmb.Items.Add("peak");
                    cmb.Text = "average"; //this is the default option
                    cmb.Location = new Point(5, 59);
                    cmb.Size = new Size(100, 24);
                    cmb.Tag = module_drag.Tag;
                    module_drag.Controls.Add(cmb);

                    cmb.SelectedIndexChanged += new System.EventHandler(this.calc_option_change);
                    // MessageBox.Show(toolConfigs[numModule].Name);
                }

                this.Controls.Add(module_drag);
                Modules.Add(module_drag);

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

            // datafile.Close();   //close file after read, don't ask why (-_-)

            /*
             * Re-size my form here
             */

            this.Size = new Size(Modules.Count() * (myModule_Size.Width + 2 * myModule_Size.Width / 10), this.Height);
#if DISPLAY
            //this.Size = new Size(this.Width, this.Height + groupBox_Debug.Height + 50);
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
            if (numTool < toolConfigs.Count)    //check only add less than available tool
            {
                AddNode();
            }
            else
            {
                bt_addNode.Enabled = false;
            }

            /*
             * if the we have more node in our list then enable our 
             * remove button
             */
            if (Nodes.Count != 0)
                bt_removeNode.Enabled = true;
            else
                bt_removeNode.Enabled = false;

            /*
             * update confirm button status 
             */
            foreach (var item in availableNodes)
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

            add_remove_result(tools_result, false);

            /*
             * Resize my Form when add node is pressed 
             */
            var size_width = Nodes.Count() * (myNode_Size.Width + myNode_Size.Width / 10) + 2 * DEF_NodeLoc.X;
            if (size_width > this.Width)
                this.Size = new Size(size_width, this.Height);

        }

        /* Button click event to remove new node to my form */
        private void bt_removeNode_Click(object sender, EventArgs e)
        {

            RemoveNode();

            add_remove_result(tools_result, false);

            /*
             * Resize my form when remove node is pressed
             */
            var size_width = Nodes.Count() * (myNode_Size.Width + myNode_Size.Width / 10) + 2 * DEF_NodeLoc.X;
            if (size_width > (Modules.Count * (myModule_Size.Width + myModule_Size.Width / 10) + DEF_ModuleLoc.X))
                this.Size = new Size(this.Width - (myNode_Size.Width + myNode_Size.Width / 10) + DEF_ModuleLoc.X / 2, this.Height);
        }

        /* Button click event to confirm the tool selection on each node */
        private void bt_confirmNode_Click(object sender, EventArgs e)
        {
            /*
             * remove all my tool everytime user confirm new setup
             */
            Tools.Clear();   // just in case

            /*
             * loop to add properties for my tool
             */
            foreach (var ToolId in modLocations)
                Tools.Add(toolConfigs[ToolId - 1]);

            /*
             * only for debug, nothing special
             */
            //debug_toolName.Text = String.Empty;
            //debug_toolVoltage.Text = String.Empty;
            //debug_toolSink.Text = String.Empty;
            //debug_toolSource.Text = String.Empty;
            //debug_calcOption.Text = String.Empty;
            /*
             * only for debug, nothing special
             */

            //foreach (var newtool in Tools)
            //{
            //    debug_toolName.Text += " " + newtool.Name;
            //    debug_toolVoltage.Text += " " + newtool.Power;
            //    debug_toolSink.Text += " " + newtool.Sink;
            //    debug_toolSource.Text += " " + newtool.Source;
            //    debug_calcOption.Text += " " + newtool.Load;
            //}

            /*
             * Enable my Calculation 
             */
             
            bt_runPython.Enabled = true;

            add_remove_result(tools_result, false);


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
            //File.WriteAllText(python_path + @"\tools_setup.txt", string.Empty);

            //foreach (var newtool in Tools)
            //{
            //    File.AppendAllText(python_path + @"\tools_setup.txt",
            //        "tool_name:" + newtool.Name + ' ' +
            //        "sink:" + newtool.Sink.ToString() + ' ' +
            //        "source:" + newtool.Source.ToString() + ' ' +
            //        "enable:" + newtool.Enable.ToString() + ' ' +
            //        "pwr_calc:" + newtool.Load + Environment.NewLine);
            //}

            StringBuilder sb = new StringBuilder();
            StringWriter str_writer = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(str_writer))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                foreach (var tool in Tools)
                {
                    writer.WritePropertyName(tool.Name);
                    writer.WriteStartObject();
                    writer.WritePropertyName("Id");
                    writer.WriteValue(tool.id);

                    if (tool.Sink == true)
                    {
                        writer.WritePropertyName("Load");
                        if (tool.Load == "average")
                            writer.WriteValue(database[tool.Name]["Load"]["Paverage"]);
                        else
                            writer.WriteValue(database[tool.Name]["Load"]["Ppeak"]);
                    }

                    if (tool.Source == "Gen")
                    {
                        writer.WritePropertyName("Generator");
                        writer.WriteValue(database[tool.Name]["Source"]["Vgenerator"]);
                        
                    }

                    if (tool.Source == "Batt")
                    {
                        writer.WritePropertyName("Battery");

                        if (tool.BatteryHealth == "Vcharged")
                        {
                            //MessageBox.Show("Vcharged");
                            writer.WriteValue(database[tool.Name]["Source"]["Vcharged"]);
                        }
                        else
                        {
                            //MessageBox.Show("Vdepleted");
                            writer.WriteValue(database[tool.Name]["Source"]["Vdepleted"]);
                        }
                            
                    }

                    writer.WritePropertyName("Connected");
                    writer.WriteValue(tool.Enable);

                    writer.WriteEndObject();
                }
                
            }

            File.WriteAllText(python_path + @"\setup.json", sb.ToString());

            /* 
            * Create batch file that run my python script
            */
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
                MessageBox.Show("Error Occured");
            }

            try
            {

                tools_result = get_tool_ltspice(python_path, "calculation.txt");

                add_remove_result(tools_result, true);

                MessageBox.Show("Succesfully executed !","Important Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Error! Abort the run!","Critial Warning", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            try
            {
                List<string> analysis_report = get_analysis_report(python_path, "report.txt");
                string report = String.Empty;

                foreach(string str in analysis_report)
                {
                    report += str + Environment.NewLine;
                }
                MessageBox.Show(report,"Analysis Report");
            }
            catch
            {
                MessageBox.Show("Error! Unable to get analysis report.");
            }

        }

        private void bt_LoadSetup_Click(object sender, EventArgs e)
        {
            
            var index = 0;
            if (numTool < toolConfigs.Count)
            {
                AddNode();
                AddNode();
                AddNode();
                AddNode();
            }

            foreach (var node in Nodes)
            {
                GroupBox module_sel = Modules[index];

                Point module_newLoc = new Point(node.Location.X, node.Location.Y);

                module_sel.Location = module_newLoc;

                SwapTool(module_sel);

                ScanTool(module_sel);

                index++;

            }
        }

        private List<string> get_tool_ltspice(string path, string str_file)
        {
            /* 
             * read data information in the text file first  
             */
            StreamReader datafile = new StreamReader(path + @"\" + str_file); //should use relative path here

            List<string> tools_config = new List<string>();

            string line = String.Empty;

            while ((line = datafile.ReadLine()) != null)
            {
                //txb_DownHole_Read.AppendText(line + Environment.NewLine);
                tools_config.Add(line);
            }

            tools_config.RemoveAt(0);   //remove the time stamp

            datafile.Close();

            return tools_config;
        }   
        private void add_remove_result(List<string> tools, bool add_remove)
        {
            int index = 0;

            if (add_remove)
            {
                for (int i = 0; i < Tools.Count(); i++)
                {
                    /* Create label for my result */
                    Label result_frame = new Label();
                    /* initialize node properties */
                    Point result_newLoc = new Point(Nodes[i].Location.X, Nodes[i].Location.Y + Nodes[i].Height + 25);

                    result_frame.Location = result_newLoc; //default location 
                    
                    result_frame.Name = "lb_result" + (numTool + 1).ToString();

                    string[] words = tools[index].Split(delimiter_for_result);

                    foreach (var word in words)
                    {
                        result_frame.Text += word + Environment.NewLine;
                        // tool_verify(word, 'V');
                    }

                    index += 1;   
                    
                    Size lb_size = new Size(myNode_Size.Width, 200);

                    result_frame.Size = lb_size;

                    /* add label to my form */
                    this.Controls.Add(result_frame);

                    lb_results.Add(result_frame);
                }
            }
            else
            {
                try
                {
                    foreach (var result in lb_results)
                    {
                        /*
                         * remove the node out of my form
                         */
                        this.Controls.Remove(result);
                    }
                    lb_results.Clear();
                }
                catch
                {
                    MessageBox.Show("Nothing to remove");
                }

            }

        }

        private List<string> get_analysis_report(string path, string str_file)
        {
             /* 
             * read data information in the text file first  
             */
            StreamReader datafile = new StreamReader(path + @"\" + str_file); //should use relative path here

            List<string> analysis_report = new List<string>();

            string line = String.Empty;

            while ((line = datafile.ReadLine()) != null)
            {
                //txb_DownHole_Read.AppendText(line + Environment.NewLine);
                analysis_report.Add(line);
            }

            analysis_report.RemoveAt(0);   //remove the time stamp

            datafile.Close();

            return analysis_report;
        }

        private void battery_health_change(object sender, System.EventArgs e)
        {
            ComboBox cmb_sel = sender as ComboBox;
            toolConfigs[Convert.ToInt32(cmb_sel.Tag) - 1].BatteryHealth = cmb_sel.SelectedItem.ToString();
            //MessageBox.Show(toolConfigs[Convert.ToInt32(cmb_sel.Tag) - 1].BatteryHealth);
        }
        private void calc_option_change(object sender, System.EventArgs e)
        {
            ComboBox cmb_sel = sender as ComboBox;

            /* change configuration cmb changed */
            toolConfigs[Convert.ToInt32(cmb_sel.Tag) - 1].Load = cmb_sel.SelectedItem.ToString();

            /*
            MessageBox.Show("Tool: " 
                + toolConfigs[Convert.ToInt32(cmb_sel.Tag) - 1].Name
                + " has changed power calculation option to " 
                + toolConfigs[Convert.ToInt32(cmb_sel.Tag) - 1].pwr_calc
                + " power");
            */
        }

        private void enable_option_change(object sender, System.EventArgs e)
        {
            CheckBox chbox = sender as CheckBox;
            /* change configuration cmb changed */
            toolConfigs[Convert.ToInt32(chbox.Tag) - 1].Enable = chbox.Checked;

            /*
            MessageBox.Show("Tool: "
                + toolConfigs[Convert.ToInt32(chbox.Tag) - 1].Name
                + " has been "
                + toolConfigs[Convert.ToInt32(chbox.Tag) - 1].enable.ToString()
                );
            */
            
        }
        /* tool drag - drop event , when left mouse down */
        private void module_drag_MouseDown(object sender, MouseEventArgs e)
        {
            /*
             * Make my life easier, don't ask why ?
             */
            GroupBox module_sel = sender as GroupBox;

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
            GroupBox module_sel = sender as GroupBox;

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
            GroupBox module_sel = sender as GroupBox;

            isDragged = false;

            SwapTool(module_sel);

            ScanTool(module_sel);

        }

        private void AddNode()
        {
            bt_addNode.Enabled = true;
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
            Nodes.Add(node_frame);
            /* increase index and update location for next node */
            numTool++;
            myNode_Loc.X = DEF_NodeLoc.X + numTool * (myNode_Size.Width + myNode_Size.Width / 10); //update location for next node
            /* Create my avaiable Node */
            availableNodes.Add(false); //default 
            modLocations.Add(0); // default
        }
        private void RemoveNode()
        {
            /*
             * remove the node out of my form
             */
            this.Controls.Remove(Nodes[Nodes.Count - 1]);

            /*
             * remove the node out of my list of object
             */
            Nodes.RemoveAt(Nodes.Count - 1);
            availableNodes.RemoveAt(availableNodes.Count - 1);
            modLocations.RemoveAt(modLocations.Count - 1);

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
            if (Nodes.Count == 0)
            {
                bt_removeNode.Enabled = false;
            }
            else
            {
                bt_removeNode.Enabled = true;
                bt_addNode.Enabled = true;
            }


            /*
             * update confirm button status 
             */
            foreach (var item in availableNodes)
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
        }
        private void ScanTool(object sender)
        {
            GroupBox module_sel = sender as GroupBox;

            bool node_occupied = false;

            int node_index = 0;

            /* 
             * re-scan all my node everytime,
             * user move the tool around
             */

            foreach (var node in Nodes)
            {

                foreach (var module in Modules)
                {
                    // MessageBox.Show("X: " + item.Location.X.ToString() + "Y: " + item.Location.Y.ToString());
                    if (module.Location.X >= node.Location.X
                        && module.Location.X <= (node.Location.X + myNode_Size.Width)
                        && module.Location.Y >= node.Location.Y
                        && module.Location.Y <= (node.Location.Y + myNode_Size.Height))
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
                        availableNodes[node_index] = true;
                        modLocations[node_index] = Convert.ToInt32(module.Tag);

                        /* 
                         * Update the node status 
                         */
                         if(toolConfigs[Convert.ToInt32(module.Tag) - 1].Source == "Gen" || toolConfigs[Convert.ToInt32(module.Tag) - 1].Source == "Batt")
                            node.BackColor = Color.LightSalmon;
                         else
                            node.BackColor = Color.LightSkyBlue;

                        /*
                         * only for debug, display out my form
                         */
                        foreach (var status in availableNodes)
                            debug_avaiableNode.Text += " " + status.ToString();

                        foreach (var id in modLocations)
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
                    availableNodes[node_index] = false;
                    modLocations[node_index] = 0;
                    node.BackColor = SystemColors.Control;

                    debug_avaiableNode.Text = String.Empty;
                    debug_NodeID.Text = String.Empty;

                    foreach (var status in availableNodes)
                        debug_avaiableNode.Text += " " + status.ToString();

                    foreach (var id in modLocations)
                        debug_NodeID.Text += " " + id.ToString();
                }


                node_index++;   // increase index for next node 

                node_occupied = false;  // 
            }

            /* 
             * update new confirm button
             */
            foreach (var item in availableNodes)
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
        private void SwapTool(object sender)
        {
            GroupBox module_sel = sender as GroupBox;

            //bool node_occupied = false;

            //int node_index = 0;

            /*
             * Swapping module
             */
            int myId = 0;
            foreach (var node in Nodes)
            {
                if (availableNodes[myId] == true
                        && module_sel.Location.X >= node.Location.X
                        && module_sel.Location.X <= (node.Location.X + myNode_Size.Width)
                        && module_sel.Location.Y >= node.Location.Y
                        && module_sel.Location.Y <= (node.Location.Y + myNode_Size.Height))

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

                    Modules[modLocations[myId] - 1].Location = drag_Loc;


                    break;
                }
                myId++;
            }
        }

    }
}
