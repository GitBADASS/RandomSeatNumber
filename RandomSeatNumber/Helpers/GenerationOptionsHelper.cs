using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RandomSeatNumber.Helpers
{
    public class GenerationOptionsHelper
    {
        private const string RepeatabilityKey = "IsRepeatable";
        private const string GenerationFreqKey = "GenerationFreq";
        public static bool IsRepeatable
        {
            get
            {
                Object res = ApplicationData.Current.LocalSettings.Values[RepeatabilityKey];
                
                if (res == null)
                {
                    return false;
                }

                return Convert.ToBoolean(res);
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[RepeatabilityKey] = value.ToString();
            }
        }

        public static int GenerationFreq
        {
            get
            {
                Object res = ApplicationData.Current.LocalSettings.Values[GenerationFreqKey];

                if (res == null)
                {
                    return 1;
                }

                return Convert.ToInt32(res);
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values[GenerationFreqKey] = value.ToString();
            }    
                else
                {
                    ApplicationData.Current.LocalSettings.Values.Add(GenerationFreqKey, value.ToString());
                }
                  
            }    
        }
    }
}
