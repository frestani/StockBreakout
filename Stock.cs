using System;
using System.Collections.Generic;
using System.Text;
using static EventDemo.Utility;

namespace EventDemo
{
    class Stock
    {
        string symbol;
        decimal price;

        public Stock(string symbol) { this.symbol = symbol; }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }

        public void PriceFluctuation()
        {
            //make price fluctuation more common?
            if (GetRandomInteger(100) >= 75)
            {
                Price = Price + GetRandomInteger(-20, 20);
            }
            ////make it less common?
            //if (GetRandomInteger(100) <= 25)
            //{
            //    Price = Price + GetRandomInteger(-5, 5);
            //}

            //if (GetRandomInteger(2)==1)
            //Price = Price + GetRandomInteger(-10, 10);

            //change the range the price will fluctuate depending on whether it is more or less common

        }


        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }

        public string Symbol { get => symbol; set => symbol = value; }

        public void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            //add different statements - print a unique message if stock increases
            if (e.LastPrice < e.NewPrice)
                Print($"{Symbol} price has increased. Previous price was {e.LastPrice}, new price is {e.NewPrice}.");
            //print another message if it decreases
            if (e.LastPrice > e.NewPrice)
                Print($"{Symbol} price has decreased. Previous price was {e.LastPrice}, new price is {e.NewPrice}.");

            if (e.LastPrice != e.NewPrice)
                Print($"{Symbol} price has changed. Previous price was {e.LastPrice}, new price is {e.NewPrice}. The difference is {e.LastPrice - e.NewPrice}.");
        }

        public class PriceChangedEventArgs : EventArgs
        {
            public readonly decimal LastPrice;
            public readonly decimal NewPrice;

            public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
            {
                LastPrice = lastPrice; NewPrice = newPrice;
            }
        }

    }
}
