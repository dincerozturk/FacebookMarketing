using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FM.Application.Constants;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public static class StringHelper
    {
        private static List<string> listForeignCity = new List<string>()
        {
            "newyork",
            "tokyo"
        };
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase) return builder.ToString().ToLower();

            return builder.ToString();
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: return null;

                case "": return "";

                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string CareerOccupation(string input)
        {
            var result = string.Empty;
            try
            {
                result = String.Join("", input.Split(',')).Trim();
                result = Regex.Replace(result, @"\s+", ",");
            }
            catch (Exception e)
            {
                // ignored
            }

            return result;
        }

        public static string JoinWithProperty<T>(T src, string probName, string fieldName)
        {
            string result = null;
            try
            {
                var listPropertyObject = src.GetType().GetProperties();

                foreach (var item in listPropertyObject)
                {
                    if (item.Name == probName)
                    {
                        var listByProbName = item.GetValue(src);
                        var listTest = ((IEnumerable)listByProbName).Cast<object>().ToList();

                        var temp = listTest
                            .Where(x => !string.IsNullOrEmpty(x.GetType().GetProperty(fieldName)?.GetValue(x)?.ToString()))
                            .Select(p => p.GetType().GetProperty(fieldName)?.GetValue(p)?.ToString());

                        result = string.Join(", ", temp);

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            return result;
        }

        public static string GetBaseCity(this string input)
        {
            if (string.IsNullOrEmpty(input) || input == AppConstant.NA)
                return AppConstant.NA;

            if (input.CheckContainExtension("angiang|an giang"))
                return "An Giang";

            if (input.CheckContainExtension("baria-vungtau|baria|bai ria|vung tau"))
                return "Bà Rịa Vũng Tàu";

            if (input.CheckContainExtension("bacgiang|bac giang"))
                return "Bắc Giang";

            if (input.CheckContainExtension("backan|baccan|bac kan|bac can"))
                return "Bắc Cạn";

            if (input.CheckContainExtension("baclieu|bac lieu"))
                return "Bạc Liêu";

            if (input.CheckContainExtension("bacninh|bac ninh"))
                return "Bắc Ninh";

            if (input.CheckContainExtension("bentre|ben tre"))
                return "Bến Tre";

            if (input.CheckContainExtension("binhdinh|binh dinh"))
                return "Bình Định";

            if (input.CheckContainExtension("binhduong|binh duong"))
                return "Bình Dương";

            if (input.CheckContainExtension("binhphuoc|binh phuoc"))
                return "Bình Phước";

            if (input.CheckContainExtension("binhthuan|binh thuan"))
                return "Bình Thuận";

            if (input.CheckContainExtension("camau|ca mau"))
                return "Cà Mau";

            if (input.CheckContainExtension("cantho|can tho"))
                return "Cần Thơ";

            if (input.CheckContainExtension("caobang|cao bang"))
                return "Cao Bằng";

            if (input.CheckContainExtension("danang|da nang"))
                return "Đà Nẵng";

            if (input.CheckContainExtension("daclak|daclac|daklak|daklac|dac lak|dac lac|dak lak|dak lac"))
                return "Đắk Lắk";

            if (input.CheckContainExtension("dacnong|daknong|dac nong|dak nong"))
                return "Đắk Nông";

            if (input.CheckContainExtension("dienbien|dien bien"))
                return "Điện Biên";

            if (input.CheckContainExtension("dongnai|dong nai"))
                return "Đồng Nai";

            if (input.CheckContainExtension("dongthap|dong thap"))
                return "Đồng Tháp";

            if (input.CheckContainExtension("gialai|gia lai"))
                return "Gia Lai";

            if (input.CheckContainExtension("hagiang|ha giang"))
                return "Hà Giang";

            if (input.CheckContainExtension("hanam|ha nam"))
                return "Hà Nam";

            if (input.CheckContainExtension("hanoi|ha noi|hatay|hay tay"))
                return "Hà Nội";

            if (input.CheckContainExtension("hatinh|ha tinh"))
                return "Hà Tĩnh";

            if (input.CheckContainExtension("haiduong|hai duong"))
                return "Hải Dương";

            if (input.CheckContainExtension("haiphong|hai phong"))
                return "Hải Phòng";

            if (input.CheckContainExtension("haugiang|hau giang"))
                return "Hậu Giang";

            if (input.CheckContainExtension("hochiminh|ho chi minh|hcm|saigon|sg"))
                return "Hồ Chí Minh";

            if (input.CheckContainExtension("hoabinh|hoa binh"))
                return "Hòa Bình";

            if (input.CheckContainExtension("hungyen|hung yen"))
                return "Hưng Yên";

            if (input.CheckContainExtension("khanhhoa|khanh hoa"))
                return "Khánh Hòa";

            if (input.CheckContainExtension("kiengiang|kien giang"))
                return "Kiên Giang";

            if (input.CheckContainExtension("kontum|kon tum"))
                return "Kon Tum";

            if (input.CheckContainExtension("laichau|lai chau"))
                return "Lai Châu";

            if (input.CheckContainExtension("lamdong|lam dong"))
                return "Lâm Đồng";

            if (input.CheckContainExtension("langson|lang son|huulung|huu lung"))
                return "Lạng Sơn";

            if (input.CheckContainExtension("laocai|lao cai"))
                return "Lào Cai";

            if (input.CheckContainExtension("longan|long an"))
                return "Long An";

            if (input.CheckContainExtension("namdinh|nam dinh"))
                return "Nam Định";

            if (input.CheckContainExtension("nghean|nghe an"))
                return "Nghệ An";

            if (input.CheckContainExtension("ninhbinh|ninh binh"))
                return "Ninh Bình";

            if (input.CheckContainExtension("ninhthuan|ninh thuan"))
                return "Ninh Thuận";

            if (input.CheckContainExtension("phutho|phu tho"))
                return "Phú Thọ";

            if (input.CheckContainExtension("phuyen|phu yen"))
                return "Phú Yên";

            if (input.CheckContainExtension("quangbinh|quang binh"))
                return "Quảng Bình";

            if (input.CheckContainExtension("quangnam|quang nam"))
                return "Quảng Nam";

            if (input.CheckContainExtension("quangngai|quang ngai"))
                return "Quảng Ngãi";

            if (input.CheckContainExtension("quangninh|quang ninh"))
                return "Quảng Ninh";

            if (input.CheckContainExtension("quangtri|quang tri"))
                return "Quảng Trị";

            if (input.CheckContainExtension("soctrang|soc trang"))
                return "Sóc Trăng";

            if (input.CheckContainExtension("sonla|son la"))
                return "Sơn La";

            if (input.CheckContainExtension("tayninh|tay ninh"))
                return "Tây Ninh";

            if (input.CheckContainExtension("thaibinh|thai binh"))
                return "Thái Bình";

            if (input.CheckContainExtension("thainguyen|thai nguyen"))
                return "Thái Nguyên";

            if (input.CheckContainExtension("thanhhoa|thanh hoa"))
                return "Thanh Hóa";

            if (input.CheckContainExtension("thuathien-hue|thua thien hue|hue"))
                return "Thừa Thiên Huế";

            if (input.CheckContainExtension("tiengiang|tien giang"))
                return "Tiền Giang";

            if (input.CheckContainExtension("travinh|tra vinh"))
                return "Trà Vinh";

            if (input.CheckContainExtension("tuyenquang|tuyen quang"))
                return "Tuyên Quang";

            if (input.CheckContainExtension("vinhlong|vinh long"))
                return "Vĩnh Long";

            if (input.CheckContainExtension("vinhphuc|vinh phuc"))
                return "Vĩnh Phúc";

            if (input.CheckContainExtension("yenbai|yen bai"))
                return "Yên Bái";

            return input;
        }


        public static bool CheckContainExtension(this string inputOrigin, string text)
        {
            var arr = text.Split('|');

            foreach (var item in arr)
            {
                if (Regex.IsMatch(inputOrigin, item, RegexOptions.IgnoreCase))
                    return true;
            }

            return false;
        }

        public static string ConvertVN(this string chucodau)
        {
            try
            {
                const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
                const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
                int index = -1;

                char[] arrChar = FindText.ToCharArray();

                while ((index = chucodau.IndexOfAny(arrChar)) != -1)
                {
                    int index2 = FindText.IndexOf(chucodau[index]);
                    chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
                }

                return chucodau;
            }
            catch (Exception e)
            {
                // ignored
            }

            return chucodau;
        }

        public static string RemoveAllWhiteSpaceString(this string input)
        {
            try
            {
                input = Regex.Replace(input, @"\s+", "");

                return Regex.Replace(input, "[-]+", "", RegexOptions.Compiled);
            }
            catch (Exception e)
            {

            }

            return input;
        }

        public static string GetPostIdFromLinkPost(string postLink, ref string postLinkStr)
        {
            if (string.IsNullOrEmpty(postLink))
            {
                return postLink;
            }

            int digitCheck = 1000000;
            var tempPostLink = postLink;
            postLinkStr = postLink.Last() == '/' ? postLink.Substring(0, postLink.Length - 1) : postLink;
            postLinkStr = postLinkStr.Replace("/?d=n", "");
            postLinkStr = postLinkStr.Replace("/?sfns=mo", "");

            // xử lý cắt chuỗi -> lấy Post ID
            var postId = postLinkStr.Substring(postLinkStr.LastIndexOf("/") + 1, postLinkStr.Length - postLinkStr.LastIndexOf("/") - 1);
            if (postId.IndexOf("?") > -1)
                postId = postId.Substring(0, postId.IndexOf("?"));
            if (postId.IndexOf("_") > -1)
                postId = postId.Substring(postId.LastIndexOf("_") + 1, postId.Length - postId.LastIndexOf("_") - 1);
            if (postLinkStr.IndexOf("story_fbid=") > -1)
                postId = postLinkStr.Substring(postLinkStr.LastIndexOf("story_fbid=") + 11, postLinkStr.Length - postLinkStr.LastIndexOf("story_fbid=") - 11);
            if (postId.IndexOf("?") > -1)
                postId = postId.Substring(0, postId.IndexOf("?"));

            postId = postId.Trim().Replace("%20", "");


            postId = postId.Trim().Replace("&t=blank", "");
            postId = postId.Trim().Replace("/", "");

            //QuangTV check link format
            if (String.IsNullOrEmpty(postId))
            {
                var listNumberValue = Regex.Matches(tempPostLink, @"\d+")
                    .OfType<Match>()
                    .Where(m => long.Parse(m.Value) > digitCheck)
                    .Select(m => m.Value).ToList();
                postId = listNumberValue.LastOrDefault().Trim();
            }

            if (!Regex.IsMatch(postId, @"^\d+$"))
            {
                var listNumberValue = Regex.Matches(tempPostLink, @"\d+")
                    .OfType<Match>()
                    .Where(m => long.Parse(m.Value) > digitCheck)
                    .Select(m => m.Value).ToList();
                postId = listNumberValue.FirstOrDefault().Trim();
            }
            return postId;
        }

        public static string GetPostIdFromPostLinkV2(string postLink)
        {
            var postId = "";
            try
            {
                if (string.IsNullOrEmpty(postLink))
                {
                    return postLink;
                }
                int digitCheck = 1000000;
                postLink = postLink.Last() == '/' ? postLink.Substring(0, postLink.Length - 1) : postLink;
                postLink = postLink.Replace("/?d=n", "");
                postLink = postLink.Replace("/?sfns=mo", "");
                // xử lý cắt chuỗi -> lấy Post ID
                postId = postLink.Substring(postLink.LastIndexOf("/") + 1, postLink.Length - postLink.LastIndexOf("/") - 1);
                if (postId.IndexOf("?") > -1)
                    postId = postId.Substring(0, postId.IndexOf("?"));
                if (postId.IndexOf("_") > -1)
                    postId = postId.Substring(postId.LastIndexOf("_") + 1, postId.Length - postId.LastIndexOf("_") - 1);
                if (postLink.IndexOf("story_fbid=") > -1)
                    postId = postLink.Substring(postLink.LastIndexOf("story_fbid=") + 11, postLink.Length - postLink.LastIndexOf("story_fbid=") - 11);
                if (postId.IndexOf("?") > -1)
                    postId = postId.Substring(0, postId.IndexOf("?"));

                postId = postId.Trim().Replace("%20", "");


                postId = postId.Trim().Replace("&t=blank", "");
                postId = postId.Trim().Replace("/", "");

                //QuangTV check link format
                if (String.IsNullOrEmpty(postId))
                {
                    var listNumberValue = Regex.Matches(postLink, @"\d+")
                        .OfType<Match>()
                        .Where(m => long.Parse(m.Value) > digitCheck)
                        .Select(m => m.Value).ToList();
                    postId = listNumberValue.LastOrDefault().Trim();
                }

                if (!Regex.IsMatch(postId, @"^\d+$"))
                {
                    var listNumberValue = Regex.Matches(postLink, @"\d+")
                        .OfType<Match>()
                        .Where(m => long.Parse(m.Value) > digitCheck)
                        .Select(m => m.Value).ToList();
                    postId = listNumberValue.FirstOrDefault().Trim();
                }

            }
            catch (Exception e)
            {
                // ignored
            }
            return postId;
        }

        public static long GetOnlyUidComment(string input)
        {
            long result = 0;
            var digitCheck = 10000;
            try
            {
                var listNumberValue = Regex.Matches(input, @"\d+")
                    .OfType<Match>()
                    .Where(m => long.Parse(m.Value) > digitCheck)
                    .Select(m => m.Value).ToList();
                result = long.Parse(listNumberValue?.LastOrDefault()?.Trim());

            }
            catch (Exception e)
            {

            }

            return result;
        }

        public static string ConvertPostLinkToMobileVersion(string input)
        {
            var result = "";
            try
            {
                result = input.Replace("://www.", "://m.");
            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}