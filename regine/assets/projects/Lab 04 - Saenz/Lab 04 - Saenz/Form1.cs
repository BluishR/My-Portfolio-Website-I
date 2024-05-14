using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab_04___Saenz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Define product list
        private string[,] strProducts = {
                                            {"Wintermelon Special","135.00","155.00"},
                                            {"Wintermelon Overload", "155.00", "175.00"},
                                            {"Green Apple", "90.00", "100.00"},
                                            {"Lychee", "90.00","100.00"},
                                            {"Matcha", "110.00", "125.00"}};
    
        //Define enum size
        enum DrinkSize
        {
            Regular = 1,
            Large
        };

        private string fmtHead = "{0, -25}{1, -8}{2, 10}{3, 4}{4, 15}"; //Bill format
        private decimal decTotal = 0M; //Class level total
        private string strDrinks = "";
        private decimal decPrice = 0M;
        DrinkSize size; //Drink size

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load each product name into the combobox
            for (int i = 0; i < strProducts.GetLength(0); i++)
            {
                cboDrinks.Items.Add(strProducts[i,0]);
            }
            //Set the default selection to Wintermelon Special
            cboDrinks.SelectedIndex = 0;

            //Assign size to combobox
            cboSize.Items.Add(DrinkSize.Regular);
            cboSize.Items.Add(DrinkSize.Large);
            //Set the default selection to Wintermelon Regular
            cboSize.SelectedIndex = 0;

            //Show Receipt header
            lstBill.Items.Add("XYZ Refreshments");
            lstBill.Items.Add("Customer Bill");
            lstBill.Items.Add("");
            lstBill.Items.Add(string.Format(fmtHead, "Items", "Size", "Price", "Qty", "Sub Total"));
            lstBill.Items.Add("--------------------------------------------------------------------------------------------------------------------");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtQty.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter the number of Drinks" +
                    "\n", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtQty.Text = "";
                txtQty.Focus();
                return;
            }
                decimal Quantity = .0m;
                btnCompute.Enabled = true;
                try
                {
                    Quantity = decimal.Parse(txtQty.Text);
                }

                catch (Exception)
                {
                    MessageBox.Show("Check and Enter the Number of Drinks" +
                        "\n", "Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    txtQty.Text = "";
                    txtQty.Focus();
                    return;
                }

                int qty = 0;
                qty = int.Parse(txtQty.Text);
                decimal subtotal = 0M;

                switch (strDrinks)
                {
                    case "Wintermelon Special":
                        // get drink price based on size
                        if (size == DrinkSize.Regular)
                            decPrice = 135;
                        else
                            decPrice = 155;
                        break;

                    case "Wintermelon Overload":
                        // get drink price based on size
                        if (size == DrinkSize.Regular)
                            decPrice = 155;
                        else
                            decPrice = 175;
                        break;

                    case "Green Apple":
                        // get drink price based on size
                        if (size == DrinkSize.Regular)
                            decPrice = 90;
                        else
                            decPrice = 100;
                        break;

                    case "Lychee":
                        // get drink price based on size
                        if (size == DrinkSize.Regular)
                            decPrice = 90;
                        else
                            decPrice = 100;
                        break;

                    case "Matcha":
                        // get drink price based on size
                        if (size == DrinkSize.Regular)
                            decPrice = 110;
                        else
                            decPrice = 125;
                        break;
                    //Add cases for other products
                }
                subtotal = decPrice * qty; //compute sub total
                //increment total
                decTotal += subtotal;
                //add item to receipt
                lstBill.Items.Add(string.Format(fmtHead, strDrinks,
                    size, decPrice.ToString("n2"), qty.ToString("n1"),
                    subtotal.ToString("n2")));
            
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
        btnCompute.Enabled = false;
        lstBill.Items.Add("");
        lstBill.Items.Add("--------------------------------------------------------------------------------------------------------------------------");
        decimal tax = decTotal * .1M;
        lstBill.Items.Add(string.Format(fmtHead, "", "", "TAX", "", tax.ToString("n2")));
        decTotal += tax;
        lstBill.Items.Add(string.Format(fmtHead, "", "", "TOTAL", "", decTotal.ToString("n2")));

            //printpreview
         printPreviewDialog1.Document = printDocument1;
         printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
         printPreviewDialog1.ShowDialog();
        }

        //double-click cboDrinks to automatically generate this method
        //fill required code
        private void cboDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            strDrinks = cboDrinks.Text;
        }

        //double click cboSize to automatically generate this method
        //fill the required code
        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = (DrinkSize)cboSize.SelectedIndex + 1; // assign size
            //call the GetPrice method Identify the price
            decPrice = GetPrice(cboSize.SelectedIndex, size);
        }

        private decimal GetPrice(int intDrink,DrinkSize size)
        {
 	        decimal price = 0M;
            if (size == DrinkSize.Regular)
                price = decimal.Parse(strProducts[intDrink,1]);
            else
                price = decimal.Parse(strProducts[intDrink, 2]);
            return price;
}

        private void button3_Click(object sender, EventArgs e)
        {
            decTotal = .0M;
            lstBill.Items.Clear();
            btnCompute.Enabled = false;

            lstBill.Items.Add("XYZ Refreshments");
            lstBill.Items.Add("Customer Bill");
            lstBill.Items.Add("");
            lstBill.Items.Add(string.Format(fmtHead, "Items", "Size", "Price", "Qty", "Sub Total"));
            lstBill.Items.Add("-------------------------------------------------------------------------------------------------------------------------------");
        }

        private void txtQty_MouseClick(object sender, MouseEventArgs e)
        {
            txtQty.SelectAll();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to close the application?" + "\n", "Exit", MessageBoxButtons.OK);
          
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int StartX = 65;
            int StartY = 65;

            for (int x = 0; x < lstBill.Items.Count; x++)
            {
                e.Graphics.DrawString(lstBill.Items[x].ToString(), lstBill.Font, Brushes.Black, StartX, StartY);
                StartY += lstBill.ItemHeight;
            }
            
            }


       

}
}