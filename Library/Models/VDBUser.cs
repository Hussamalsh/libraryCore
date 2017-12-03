using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPL.VDB.Model
{
    public static class User_Initials
    {
        public static string Get_Initials_From_Full_Name(string Full_Name)
        {
            string initials = string.Empty;
            if (!string.IsNullOrWhiteSpace(Full_Name))
            {
                try
                {
                    Full_Name = Full_Name.Trim();
                    if (Full_Name.ToUpper() == "Doctors".ToUpper()
                        || Full_Name.ToUpper() == "Doctor".ToUpper())
                    {
                        return "DRs";
                    }
                    else
                        if (Full_Name.ToUpper() == "Research Physicians".ToUpper()
                            || Full_Name.ToUpper() == "Research Physician".ToUpper())
                    {
                        return "RPs";
                    }
                    else
                        if (Full_Name.ToUpper() == "CTA"
                            || Full_Name.ToUpper() == "Clinical Staff".ToUpper()
                            || Full_Name.ToUpper() == "Clinical Trial Assistant".ToUpper())
                    {
                        return "CTa";
                    }
                    else
                        if (Full_Name.ToUpper() == "CQm"
                            || Full_Name.ToUpper() == "Quality Manager".ToUpper()
                            || Full_Name.ToUpper() == "Clinical Quality Manager".ToUpper())
                    {
                        return "CQm";
                    }
                    else
                        if (Full_Name.ToUpper() == "VRs"
                            || Full_Name.ToUpper() == "Volunteer Recruitments"
                            || Full_Name.ToUpper() == "Volunteer Recruitment")
                    {
                        return "VRs";
                    }

                    var names_by_space = Full_Name.Trim().Split(' ');
                    if (names_by_space != null && names_by_space.Length > 0)
                    {
                        // remove empty
                        names_by_space = names_by_space.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

                        if (names_by_space.Length > 3)
                        {
                            // First Character
                            initials = string.IsNullOrWhiteSpace(names_by_space[0].Trim()) ? string.Empty
                            : names_by_space[0].Trim().Substring(0, 1).ToUpper();
                            // Second Character
                            initials += string.IsNullOrWhiteSpace(names_by_space[1].Trim()) ? string.Empty
                                : names_by_space[1].Trim().Substring(0, 1).ToUpper();
                            // Last Character
                            initials += string.IsNullOrWhiteSpace(names_by_space[names_by_space.Length - 1].Trim()) ? string.Empty
                                : names_by_space[names_by_space.Length - 1].Trim().Substring(0, 1).ToUpper();
                        }
                        else
                        {
                            foreach (var name in names_by_space)
                            {
                                var names_by_dash = name.Trim().Split('-');
                                if (names_by_dash.Length > 1)
                                {
                                    // remove empty
                                    names_by_dash = names_by_dash.Where(n => !string.IsNullOrWhiteSpace(n)).ToArray();

                                    initials += string.IsNullOrWhiteSpace(names_by_dash[0].Trim()) ? string.Empty
                                        : names_by_dash[0].Trim().Substring(0, 1).ToUpper();
                                    initials += string.IsNullOrWhiteSpace(names_by_dash[1].Trim()) ? string.Empty
                                        : names_by_dash[1].Trim().Substring(0, 1).ToUpper();
                                }
                                else
                                {
                                    initials += string.IsNullOrWhiteSpace(name.Trim()) ? string.Empty
                                        : name.Trim().Substring(0, 1).ToUpper();
                                }
                            }
                            if (initials.Length > 3)
                            {
                                initials =
                                    // First Character
                                    initials.Substring(0, 1)
                                    // Second Last Character
                                    + initials.Substring(initials.Length - 2, 1)
                                    // Last Character
                                    + initials.Substring(initials.Length - 1, 1);
                            }
                        }
                    }
                    else
                    {
                        initials = Full_Name.Substring(0, 1).ToUpper();
                    }
                }
                catch (Exception) { }
            }
            return initials;
        }
    }




    [Serializable]
    public class LDAPUser
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }

        public LDAPUser() { }
    }
}
