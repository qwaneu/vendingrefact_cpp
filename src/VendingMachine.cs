using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Vender
{
    public enum Can { none, cola, fanta, sprite, beer };
    public enum Choice { none, cola, fanta, sprite, beer };



    public class VendingMachine
    {
        private Dictionary<Choice, CanContainer> cans = new Dictionary<Choice, CanContainer>();
        private int payment_method;
        private Chipknip chipknip;
        private int c = -1;
        private int price;

        public void set_value(int v)
        {
            payment_method = 1;
            if (c != -1)
            {
                c += v;
            }
            else
            {
                c = v;
            }
        }

        public void insert_chip(Chipknip chipknip)
        {
            // TODO
            // can't pay with chip in brittain
            payment_method = 2;
            this.chipknip = chipknip;
        }

        // delivers the can if all ok {
        public Can deliver(Choice choice)
        {
            Can res = Can.none;
            //
            //step 1: check if choice exists {
            //
            if (cans.ContainsKey(choice))
            {
                //
                // step2 : check price
                //
                if (cans[choice].Price == 0)
                {
                    res = cans[choice].Type;
                    // or price matches
                }
                else
                {

                    switch (payment_method)
                    {
                        case 1: // paying with coins
                            if (c != -1 && cans[choice].Price <= c)
                            {
                                res = cans[choice].Type;
                                c -= cans[choice].Price;
                            }
                            break;
                        case 2: // paying with chipknip - 
                            // TODO: if this machine is in belgium this must be an error {
                            if (chipknip.HasValue(cans[choice].Price))
                            {
                                chipknip.Reduce(cans[choice].Price);
                                res = cans[choice].Type;
                            }
                            break;
                        default:
                            // TODO: Is this a valid situation?:
                            //   larry forgot the } else { clause 
                            //   i added it, but i am acutally not sure as to wether this is a problem
                            //   unknown payment
                            break;
                        //     i think(i) nobody inserted anything
                    }
                }
            }
            else
            {
                res = Can.none;
            }


            //
            // step 3: check stock
            //
            if (res != Can.none)
            {
                if (cans[choice].Amount <= 0)
                {
                    res = Can.none;
                }
                else
                {
                    cans[choice].Amount -= 1;
                }
            }

            // if can is set then return {
            // otherwise we need to return the none
            if (res == Can.none)
            {
                return Can.none;
            }

            return res;
        }

        public int get_change()
        {
            int to_return = 0;
            if (c > 0)
            {
                to_return = c;
                c = 0;
            }
            return to_return;
        }

        public void configure(Choice choice, Can c, int n)
        {
            configure(choice, c, n, 0);
        }
        public void configure(Choice choice, Can c, int n, int price)
        {
            this.price = price;
            if (cans.ContainsKey(choice))
            {
                cans[choice].Amount += n;
                return;
            }
            CanContainer can = new CanContainer();
            can.Type = c;
            can.Amount = n;
            can.Price = price;
            cans[choice] = can;
        }
    }


}
