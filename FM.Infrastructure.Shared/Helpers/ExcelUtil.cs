using FM.Application.Exceptions;
using FM.Domain.Entities.Facebook;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FM.Infrastructure.Shared.Helpers
{
    public static class ExcelUtil
    {
        public static List<FbToken> MakeListFbTokensFromExcelFile(IWebHostEnvironment webHostEnvironment, string fileNameWithExtension)
        {
            if (string.IsNullOrWhiteSpace(webHostEnvironment.WebRootPath))
            {
                webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            string rootPath = webHostEnvironment.WebRootPath;
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            string path = rootPath + "\\" + fileNameWithExtension;

            var result = new List<FbToken>();

            FileInfo fileInfo = new FileInfo(path);

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7
            // loop through the worksheet rows and columns
            int j = 1;
            for (int i = 2; i <= rows; i++)
            {
                var id = worksheet.Cells[i, j].TryConvertInt();
                var uid = worksheet.Cells[i, j + 1].TryConvertString();
                var name = worksheet.Cells[i, j + 2].TryConvertString();
                var cookie = worksheet.Cells[i, j + 3].TryConvertString();
                var token = worksheet.Cells[i, j + 4].TryConvertString();
                var isDead = worksheet.Cells[i, j + 5].TryConvertBoolean();
                var isUsing = worksheet.Cells[i, j + 6].TryConvertBoolean();
                var lastCallTime = worksheet.Cells[i, j + 7].TryConvertDateTime();
                var totalCalled = worksheet.Cells[i, j + 8].TryConvertInt();
                var totalCalledInLastHour = worksheet.Cells[i, j + 9].TryConvertInt();
                var totalCalledInTimeFrame = worksheet.Cells[i, j + 10].TryConvertInt();
                var maxRequestInTimeFrame = worksheet.Cells[i, j + 11].TryConvertInt();
                var timeFrameMinute = worksheet.Cells[i, j + 12].TryConvertInt();
                var note = worksheet.Cells[i, j + 13].TryConvertString();
                //var fbAccountId = worksheet.Cells[i, j + 14].TryConvertInt();

                result.Add(new FbToken
                {
                    Uid = uid,
                    Name = name,
                    Cookie = cookie,
                    Token = token,
                    IsDead = isDead,
                    IsUsing = isUsing,
                    LastCalledTime = lastCallTime,
                    TotalCalled = totalCalled,
                    TotalCalledInLastHour = totalCalledInLastHour,
                    TotalCalledInTimeFrame = totalCalledInTimeFrame,
                    MaxRequestInTimeFrame = maxRequestInTimeFrame,
                    TimeFrameMinute = timeFrameMinute,
                    Note = note,
                });
            }
            return result;
        }
        public static string TryConvertString(this ExcelRange excelRange)
        {
            try
            {
                return excelRange.Value != null ? excelRange.Value.ToString() : "";
            }
            catch (Exception e)
            {
                // ignored
            }

            return "";
        }

        public static int TryConvertInt(this ExcelRange excelRange)
        {
            try
            {
                var value = int.TryParse(excelRange.Value.ToString(), out var result);

                return result;
            }
            catch (Exception e)
            {
                throw new ExceptionCustom("TryConvertInt", excelRange, e.Message, e);
            }
        }

        public static bool TryConvertBoolean(this ExcelRange excelRange)
        {
            try
            {
                var value = bool.TryParse(excelRange.Value.ToString(), out var result);

                return result;
            }
            catch (Exception e)
            {
                // ignored
            }

            return false;
        }

        public static DateTime TryConvertDateTime(this ExcelRange excelRange)
        {
            var result = DateTime.MinValue;
            try
            {
                if (excelRange.Value is not null)
                {
                    long dateNum = long.Parse(excelRange.Value.ToString());
                    result = DateTime.FromOADate(dateNum);

                    return result;
                }


            }
            catch (Exception e)
            {
                // ignored
            }

            return result;
        }
    }
}
