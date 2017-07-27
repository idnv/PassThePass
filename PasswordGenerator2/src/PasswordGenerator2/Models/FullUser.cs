using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordGenerator2.Models
{
    public class FullUser
    {
        public int ID { get; set; } 

        Dictionary<char, char> dict = new Dictionary<char, char>();

        public string finalMailID { get; set; }

        public string finalPass { get; set; }

        public bool isVerifyed { get; set; }

        public string allMail { get; set; }

        public string allPass { get; set; }

        public bool needTest { get; set; }

        public int infoID { get; set; }

        public int resAvgID { get; set; }


        /// <summary>
        /// A dictionar that maps each hebrew letter to the corresponding english letter in turms of the letter on same
        /// key
        /// </summary>
        /// 

        //public void FillDictionary()
        //{
        //    dict.Add('א', 't');
        //    dict.Add('ב', 'c');
        //    dict.Add('ג', 'd');
        //    dict.Add('ד', 's');
        //    dict.Add('ה', 'v');
        //    dict.Add('ו', 'u');
        //    dict.Add('ז', 'z');
        //    dict.Add('ח', 'j');
        //    dict.Add('ט', 'y');
        //    dict.Add('י', 'h');
        //    dict.Add('כ', 'f');
        //    dict.Add('ך', 'l');
        //    dict.Add('ל', 'k');
        //    dict.Add('מ', 'n');
        //    dict.Add('ם', 'o');
        //    dict.Add('נ', 'b');
        //    dict.Add('ן', 'i');
        //    dict.Add('ס', 'x');
        //    dict.Add('ע', 'g');
        //    dict.Add('פ', 'p');
        //    dict.Add('ף', ';');
        //    dict.Add('צ', 'm');
        //    dict.Add('ץ', '.');
        //    dict.Add('ק', 'e');
        //    dict.Add('ר', 'r');
        //    dict.Add('ש', 'a');
        //    dict.Add('ת', ',');
        //}

        public bool isNewUser()
        {
            return string.IsNullOrWhiteSpace(finalPass);
        }

        public FullUser()
        {
            finalMailID = string.Empty;
            finalPass = string.Empty;
            isVerifyed = false;
            allMail = "";
            allPass = "";
            needTest = true;
            

        }

        public FullUser(UserInfo user,string finalPass, string finalMail)
        {
            finalMailID = finalMail;
            this.finalPass = finalPass;
            isVerifyed = false;
            allMail = "";
            allPass = "";

        }

        //public FullUser(UserInfo user, string mailChanging, string finalMail, string passChanging, 
        //                string finalPass, string allPasswords)
        //{
        //    List<string> FinalPassList = new List<string>();
        //    FinalMail = finalMail;
        //    FinalPass = finalPass;
        //   AllMail = !string.IsNullOrEmpty(mailChanging) ?  mailChanging.Trim().Split(' ').ToList() : new List<string>();
        //   AllPass = !string.IsNullOrEmpty(passChanging) ? passChanging.Trim().Split(' ').ToList() : new List<string>();
        //    var passwordTries = allPasswords.Split(',').ToList();
        //    passwordTries.RemoveAll(x => string.IsNullOrEmpty(x));


        //    AllPass = FilterPasswordTries(AllPass);
        //    AllPass.AddRange(passwordTries);
        //    AllPass = AllPass.Distinct().ToList();
        //    this.FillDictionary();

        //    AllPass.ForEach(x => FinalPassList.Add(ConvertHebrewToEnglishChars(x)));
        //    FinalPassList = FinalPassList.Distinct().ToList();
        //    AllPass = FinalPassList;

        //    AllMail = FilterPasswordTries(AllMail);
        //    AllMail = AllMail.Distinct().ToList();


        //    Info = user;
        //}

        //public string ConvertHebrewToEnglishChars(string password)
        //{
        //    string newPass = "";
        //    foreach (char letter in password)
        //    {
        //        if (dict.ContainsKey(letter))
        //        {
        //            newPass += dict[letter];
        //        }
        //        else { newPass += letter; }
        //    }
        //    return newPass;
        //}

        //public List<string> FilterPasswordTries(List<string> passwordTries)
        //{
        //    List<string> minMaxStrings = new List<string>();


        //    for (int index = 1; index < passwordTries.Count - 1; index++)
        //    {
        //        if (passwordTries[index] != "passwordTries[index].Length")
        //        {
        //            if ((passwordTries[index].Length >= 2) &&
        //                (passwordTries[index - 1].Length <= passwordTries[index].Length &&
        //                passwordTries[index + 1].Length <= passwordTries[index].Length ||
        //                (passwordTries[index - 1].Length >= passwordTries[index].Length &&
        //                passwordTries[index + 1].Length >= passwordTries[index].Length)))
        //            {
        //                minMaxStrings.Add(passwordTries[index]);
        //            }
        //        }
        //    }
        //    return minMaxStrings;
        //}
    }
}