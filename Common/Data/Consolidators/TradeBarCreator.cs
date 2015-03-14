/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using QuantConnect.Data.Market;

namespace QuantConnect.Data.Consolidators
{
    /// <summary>
    /// Type capable of creating new trade bars from other trade bars
    /// </summary>
    public class TradeBarCreator : TradeBarCreatorBase<TradeBar>
    {
        /// <summary>
        /// Initializes a new instance of the TradeBarTradeBarCreator class to produce a new 'TradeBar' representing the period
        /// </summary>
        /// <param name="period">The minimum span of time before emitting a new trade bar</param>
        public TradeBarCreator(TimeSpan period)
            : base(period)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TradeBarTradeBarCreator class to produce a new 'TradeBar' representing the last count pieces of data
        /// </summary>
        /// <param name="maxCount">The number of pieces to accept before emiting a new trade bar</param>
        public TradeBarCreator(int maxCount)
            : base(maxCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TradeBarTradeBarCreator class to produce a new 'TradeBar' representing the last count pieces of data or the period, whichever comes first
        /// </summary>
        /// <param name="maxCount">The number of pieces to accept before emiting a new trade bar</param>
        /// <param name="period">The minimum span of time before emitting a new trade bar</param>
        public TradeBarCreator(int maxCount, TimeSpan period)
            : base(maxCount, period)
        {
        }

        protected override void AggregateBar(ref TradeBar workingBar, TradeBar data)
        {
            if (workingBar == null)
            {
                workingBar = new TradeBar
                {
                    Time = data.Time,
                    Symbol = data.Symbol,
                    Open = data.Open,
                    High = data.High,
                    Low = data.Low,
                    Close = data.Close,
                    Volume = data.Volume,
                    DataType = MarketDataType.TradeBar
                };
            }
            else
            {
                //Aggregate the working bar
                workingBar.Close = data.Close;
                workingBar.Volume += data.Volume;
                if (data.Low < workingBar.Low) workingBar.Low = data.Low;
                if (data.High > workingBar.High) workingBar.High = data.High;
            }
        }
    }
}