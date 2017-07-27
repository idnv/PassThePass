using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordGenerator2.Helpers;
using System.Configuration;

namespace PasswordGenerator2.Models
{
    [Serializable]
    public class UserInfo
    {

        public int ID { get; set; }

        public string mailId { get; set; }

        [Display(Name ="מין")]
        [Required(ErrorMessage = "שדה חובה")]
        public Gender gender { get; set; }

        [Display(Name="שם פרטי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string firstName { get; set; }

        [Display(Name="שם משפחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string lastName { get; set; }

        [Display(Name="מצב משפחתי")]
        [Required(ErrorMessage = "שדה חובה")]
        public Status familyStatus { get; set; }

        [Display(Name="עיר מגורים")]
        [Required(ErrorMessage = "שדה חובה")]
        public string city { get; set; }

        [Display(Name = "הודעת פתיחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string message { get; set; }
        /*
        [Display(Name="גיל")]
        [Required(ErrorMessage = "שדה חובה")]
        [Range(1, Int32.MaxValue,ErrorMessage = "הגיל חייב להיות גדול מ0")]
        public int age { get; set; }
        
         
        <div class="form-group">
            <div class="col-md-10">
                @Html.EditorFor(model => model.age, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.age, "", new { @class = "text-danger" })
            </div>
            <div>
                @Html.LabelFor(model => model.age, htmlAttributes: new { @class = "control-label col-md-2", @style = "text-align:left" })
            </div>
        </div>
        

        <div class="form-group">
            <label asp-for="age" class="col-md-2 control-label"></label>
            <div class="col-md-10">
                <input asp-for="age" class="form-control" />
                <span asp-validation-for="age" class="text-danger" />
            </div>
        </div>
         */



    }




    public enum Gender
    {
        [Display(Name = "זכר")]
        Male,
        [Display(Name = "נקבה")]
        Female
    }

    public enum Status
    {
        [Display(Name = "נשוי")]
        Married,
        [Display(Name = "רווק")]
        Single,
        [Display(Name = "גרוש")]
        Divorsed
    }
}