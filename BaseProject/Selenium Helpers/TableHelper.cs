using System;
using System.Linq;
using BaseProject.PageElementLibrary;
using OpenQA.Selenium;
using FluentAssertions;

namespace BaseProject.SeleniumHelpers
{
    public class TableHelper
    {
        public IWebElement GetRowMatchingTheInputStringPatternFromTable(string matchPattern, Element table)
        {
            Element rowObj = new Element("xpath", ".//tr");
            var rows = rowObj.GetElementCollectionFromExistingObject(table.GetElement());
            var matchedElement = rows.FirstOrDefault(row => 
                            row.GetAttribute("textContent").ToLower().Contains(matchPattern.ToLower()));
            return matchedElement;   
        }

        public int GetRowIndexOfTheMatchingInputPatternFromTable(string matchPattern, Element table)
        {
            var matchedRow = GetRowMatchingTheInputStringPatternFromTable(matchPattern, table);
            matchedRow.Should().NotBeNull();
            Element rowObj = new Element("xpath", ".//tr");
            var rows = rowObj.GetElementCollectionFromExistingObject(table.GetElement()).ToList();
            var index = rows.IndexOf(matchedRow);
            index.Should().NotBe(-1);
            return index;
        }

        public IWebElement GetRowMatchingTheInputRowNumberFromTable(string rowNumber, Element table)
        {
            Element rowObj = new Element("xpath", ".//tr");
            var rows = rowObj.GetElementCollectionFromExistingObject(table.GetElement());
            int row = Int32.Parse(rowNumber);
            if(rows.Count < row)
            {
                return null;
            }

            return rows.ElementAt(row);
        }

        public IWebElement SelectAndGetRowObjectForGivenRowItemNameOrRowNumber(string rowNumber, string rowItemToMatch, TableHelper table, Element tableGrid)
        {
             try
            {
                IWebElement rowObj;


                if (!string.IsNullOrEmpty(rowNumber))
                {
                    rowObj = table.GetRowMatchingTheInputRowNumberFromTable(rowNumber, tableGrid);
                    rowObj.Should().NotBeNull();
                    Synchronisation.WaitUntilObjectEnabled(rowObj);
                    rowObj.Click();
                }
                else if(!string.IsNullOrEmpty(rowItemToMatch))
                {
                    rowObj = table.GetRowMatchingTheInputStringPatternFromTable(rowItemToMatch, tableGrid);
                    rowObj.Should().NotBeNull();
                    Synchronisation.WaitUntilObjectEnabled(rowObj);
                    rowObj.Click();
                }
                else
                {
                    return null;
                }

                Synchronisation.WaitForPageToLoad();
                return rowObj;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        
    }
}
