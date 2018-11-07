﻿using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    public class FunctionRepository
    {
        private List<string> readRawFunctionsFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\AvailableFunctions.txt";
            var lines = File.ReadLines(path);
            var res = new List<string>();
            foreach (var line in lines)
            {
                res.Add(line);
            }
            return res;
        }

        public IList<FunctionSignature> GetCleanFunctions()
        {
            var raw = readRawFunctionsFromFile();
            var cleanRaw = cleanRawFunctions(raw);
            var res = ParseFunctionSignatures(cleanRaw);
            return res;
        }

        private List<string> cleanRawFunctions(List<string> rawFunctions)
        {
            var res = new List<string>();
            foreach (var item in rawFunctions)
            {
                if (!isValid(item)) continue;
                string cleanItem = item.Trim();
                cleanItem = Regex.Replace(cleanItem, @"\s+", " ");
                cleanItem = cleanItem.Replace("( ", "(");
                cleanItem = cleanItem.Replace(" (", "(");
                cleanItem = cleanItem.Replace(") ", ")");
                cleanItem = cleanItem.Replace(" )", ")");
                cleanItem = cleanItem.Replace(", ", ",");
                cleanItem = cleanItem.Replace(" ,", ",");
                res.Add(cleanItem);
            }
            return res;
        }

        private bool isValid(string function)
        {
            return true;
        }

        public List<FunctionSignature> ParseFunctionSignatures(List<string> signatrues)
        {
            var res = new List<FunctionSignature>();
            foreach (var item in signatrues)
            {
                res.Add(ParseFunctionSignature(item));
            }
            return res;
        }

        private FunctionSignature ParseFunctionSignature(string signature)
        {
            string functionName = (signature.Split('(')[0]).Trim();
            int functionStartIndex = signature.IndexOf("(");
            int functionEndIndex = signature.IndexOf(")");
            string allArguments = signature.Substring(functionStartIndex + 1, functionEndIndex - functionStartIndex - 1);

            string[] functionParameters = allArguments.Split(',');
            var arguments = new List<Argument>();
            foreach (var item in functionParameters)
            {
                var type = item.Split(' ')[0];
                var name = item.Split(' ')[1];
                arguments.Add(new Argument()
                {
                    Name = name,
                    Type = type
                });
            }

            return new FunctionSignature()
            {
                Name = functionName,
                Arguments = arguments
            };
        }
    }
}
