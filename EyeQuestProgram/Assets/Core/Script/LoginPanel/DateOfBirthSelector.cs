using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ใช้ TMP_Dropdown

public class DateOfBirthSelector : MonoBehaviour
{
    public TMP_Dropdown dayDropdown;
    public TMP_Dropdown monthDropdown;
    public TMP_Dropdown yearDropdown;
    private void Start()
    {
        PopulateYearDropdown();
        PopulateMonthDropdown();
        PopulateDayDropdown(); // เริ่มต้นด้วย 31 วัน

        // อัปเดตจำนวนวันตามเดือน/ปีที่เลือก
        monthDropdown.onValueChanged.AddListener(delegate { PopulateDayDropdown(true); });
        yearDropdown.onValueChanged.AddListener(delegate { PopulateDayDropdown(true); });
    }

    void PopulateYearDropdown()
    {
        yearDropdown.ClearOptions();
        List<string> years = new List<string>();
        int currentYear = DateTime.Now.Year;

        for (int y = currentYear; y >= 1900; y--)
        {
            years.Add(y.ToString());
        }

        yearDropdown.AddOptions(years);
    }

    void PopulateMonthDropdown()
    {
        monthDropdown.ClearOptions();
        List<string> months = new List<string>();

        for (int m = 1; m <= 12; m++)
        {
            months.Add(m.ToString("00"));
        }

        monthDropdown.AddOptions(months);
    }

    void PopulateDayDropdown(bool keepSelection = false)
    {
        int previousDayIndex = dayDropdown.value; // เก็บ index เดิม
        string previousDayText = previousDayIndex < dayDropdown.options.Count
            ? dayDropdown.options[previousDayIndex].text
            : "01";

        dayDropdown.ClearOptions();

        int year = int.Parse(yearDropdown.options[yearDropdown.value].text);
        int month = int.Parse(monthDropdown.options[monthDropdown.value].text);

        int daysInMonth = DateTime.DaysInMonth(year, month);
        List<string> days = new List<string>();

        for (int d = 1; d <= daysInMonth; d++)
        {
            days.Add(d.ToString("00"));
        }

        dayDropdown.AddOptions(days);

        if (keepSelection)
        {
            // ตรวจสอบว่าวันที่เดิมยังมีอยู่ไหม
            int previousDay;
            if (int.TryParse(previousDayText, out previousDay))
            {
                if (previousDay <= daysInMonth)
                {
                    dayDropdown.value = previousDay - 1; // set เป็นวันเดิม
                }
                else
                {
                    dayDropdown.value = daysInMonth - 1; // set เป็นวันสุดท้าย
                }
            }
        }
    }

    public string GetSelectedDate()
    {
        string day = dayDropdown.options[dayDropdown.value].text;
        string month = monthDropdown.options[monthDropdown.value].text;
        string year = yearDropdown.options[yearDropdown.value].text;
        return $"{day}/{month}/{year}";
    }
}