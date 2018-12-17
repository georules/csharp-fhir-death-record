﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using FhirDeathRecord;

namespace csharp_fhir_death_record
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // // Create Bundle
                // Bundle bundle = new Bundle();
                // bundle.Type = Bundle.BundleType.Document;

                // // Create Patient
                // Patient patient = new Patient();
                // patient.Id = "urn:uuid:" + Guid.NewGuid().ToString();

                // // Create new Extension
                // Extension extension = new Extension();
                // extension.Url = "Example-Extension";

                // // Create Address with State and Country
                // Address address = new Address();
                // address.State = "New York";
                // address.Type = Hl7.Fhir.Model.Address.AddressType.Postal;

                // // Add Address to Extension
                // extension.Value = address;

                // // Add Extension to Bundle
                // patient.Extension.Add(extension);

                // // Add Patient to Bundle
                // bundle.AddResourceEntry(patient, patient.Id);

                // // Serialize Bundle to JSON
                // var xmlserializer = new FhirXmlSerializer();
                // string xml = xmlserializer.SerializeToString(bundle);

                // // Serialize Bundle to XML
                // var jsonserializer = new FhirJsonSerializer();
                // string json = jsonserializer.SerializeToString(bundle);

                // Console.WriteLine(FormatJson(json) + "\n");

                // Console.WriteLine(XDocument.Parse(xml).ToString() + "\n");

                Console.WriteLine("No filepath given; Constructing a fake record and printing its XML output...\n");
                DeathRecord deathRecord = new DeathRecord();
                deathRecord.Id = "1337";
                deathRecord.DateOfRegistration = "2018-07-11";
                deathRecord.FirstName = "Example";
                deathRecord.MiddleName = "Middle";
                deathRecord.FamilyName = "Last";
                deathRecord.MaidenName = "Last Maiden";
                deathRecord.Suffix = "Sr.";
                deathRecord.FatherFamilyName = "FTHLast";
                deathRecord.Gender = "male";
                Dictionary<string, string> code = new Dictionary<string, string>();
                code.Add("code", "M");
                code.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                code.Add("display", "Male");
                deathRecord.BirthSex = code;
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("residenceLine1", "19 Example Street");
                dictionary.Add("residenceLine2", "Line 2");
                dictionary.Add("residenceCity", "Bedford");
                dictionary.Add("residenceCounty", "Middlesex");
                dictionary.Add("residenceState", "Massachusetts");
                dictionary.Add("residenceZip", "01730");
                dictionary.Add("residenceCountry", "United States");
                dictionary.Add("residenceInsideCityLimits", "True");
                deathRecord.Residence = dictionary;
                deathRecord.SSN = "111223333";
                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                deathRecord.Ethnicity = ethnicity;
                Tuple<string, string>[] race = {Tuple.Create("White", "2106-3"), Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8")};
                deathRecord.Race = race;
                deathRecord.DateOfBirth = "1970-04-24";
                deathRecord.DateOfDeath = "1970-04-24";
                dictionary = new Dictionary<string, string>();
                dictionary.Add("placeOfBirthLine1", "9 Example Street");
                dictionary.Add("placeOfBirthLine2", "Line 2");
                dictionary.Add("placeOfBirthCity", "Bedford");
                dictionary.Add("placeOfBirthCounty", "Middlesex");
                dictionary.Add("placeOfBirthState", "Massachusetts");
                dictionary.Add("placeOfBirthZip", "01730");
                dictionary.Add("placeOfBirthCountry", "United States");
                deathRecord.PlaceOfBirth = dictionary;
                dictionary = new Dictionary<string, string>();
                dictionary.Add("placeOfDeathTypeCode", "16983000");
                dictionary.Add("placeOfDeathTypeSystem", "http://snomed.info/sct");
                dictionary.Add("placeOfDeathTypeDisplay", "Death in hospital");
                dictionary.Add("placeOfDeathFacilityName", "Example Hospital");
                dictionary.Add("placeOfDeathLine1", "8 Example Street");
                dictionary.Add("placeOfDeathLine2", "Line 2");
                dictionary.Add("placeOfDeathCity", "Bedford");
                dictionary.Add("placeOfDeathCounty", "Middlesex");
                dictionary.Add("placeOfDeathState", "Massachusetts");
                dictionary.Add("placeOfDeathZip", "01730");
                dictionary.Add("placeOfDeathCountry", "United States");
                dictionary.Add("placeOfDeathInsideCityLimits", "True");
                deathRecord.PlaceOfDeath = dictionary;
                code = new Dictionary<string, string>();
                code.Add("code", "S");
                code.Add("system", "http://hl7.org/fhir/v3/MaritalStatus");
                code.Add("display", "Never Married");
                deathRecord.MaritalStatus = code;
                code = new Dictionary<string, string>();
                code.Add("code", "PHC1453");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/decedent/cs/EducationCS");
                code.Add("display", "Bachelor's Degree");
                deathRecord.Education = code;
                //deathRecord.Age = "100";
                Dictionary<string, string> occupation = new Dictionary<string, string>();
                occupation.Add("jobDescription", "Software Engineer");
                occupation.Add("industryDescription", "Information Technology");
                deathRecord.Occupation = occupation;
                deathRecord.ServedInArmedForces = false;
                dictionary = new Dictionary<string, string>();
                dictionary.Add("dispositionTypeCode", "449971000124106");
                dictionary.Add("dispositionTypeSystem", "http://snomed.info/sct");
                dictionary.Add("dispositionTypeDisplay", "Burial");
                dictionary.Add("dispositionPlaceName", "Example disposition place name");
                dictionary.Add("dispositionPlaceLine1", "100 Example Street");
                dictionary.Add("dispositionPlaceLine2", "Line 2");
                dictionary.Add("dispositionPlaceCity", "Bedford");
                dictionary.Add("dispositionPlaceCounty", "Middlesex");
                dictionary.Add("dispositionPlaceState", "Massachusetts");
                dictionary.Add("dispositionPlaceZip", "01730");
                dictionary.Add("dispositionPlaceCountry", "United States");
                dictionary.Add("dispositionPlaceInsideCityLimits", "True");
                dictionary.Add("funeralFacilityName", "Example funeral facility name");
                dictionary.Add("funeralFacilityLine1", "50 Example Street");
                dictionary.Add("funeralFacilityLine2", "Line 2a");
                dictionary.Add("funeralFacilityCity", "Watertown");
                dictionary.Add("funeralFacilityCounty", "Middlesex");
                dictionary.Add("funeralFacilityState", "Massachusetts");
                dictionary.Add("funeralFacilityZip", "02472");
                dictionary.Add("funeralFacilityCountry", "United States");
                dictionary.Add("funeralFacilityInsideCityLimits", "False");
                deathRecord.Disposition = dictionary;
                deathRecord.FamilyName = "Doctor";
                deathRecord.CertifierFirstName = "Example";
                deathRecord.CertifierMiddleName = "Middle";
                deathRecord.CertifierSuffix = "Sr.";
                Dictionary<string, string> address = new Dictionary<string, string>();
                address.Add("street", "123 Test Street");
                address.Add("city", "Boston");
                address.Add("state", "Massachusetts");
                address.Add("zip", "12345");
                deathRecord.CertifierAddress = address;
                code = new Dictionary<string, string>();
                code.Add("code", "434651000124107");
                code.Add("display", "Physician (Pronouncer and Certifier)");
                deathRecord.BirthSex = code;
                deathRecord.ContributingConditions = "Example Contributing Condition";
                Tuple<string, string, Dictionary<string, string>>[] causes =
                {
                    Tuple.Create("Example Immediate COD", "minutes", new Dictionary<string, string>(){ {"code", "1234"}, {"system", "example"} }),
                    Tuple.Create("Example Underlying COD 1", "2 hours", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 2", "6 months", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 3", "15 years", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 4", "30 years", new Dictionary<string, string>()),
                    Tuple.Create("Example Underlying COD 5", "years", new Dictionary<string, string>())
                };
                deathRecord.CausesOfDeath = causes;
                deathRecord.AutopsyPerformed = false;
                deathRecord.AutopsyResultsAvailable = false;
                code = new Dictionary<string, string>();
                code.Add("code", "7878000");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/MannerOfDeathVS");
                code.Add("display", "Accident");
                deathRecord.MannerOfDeath = code;
                code = new Dictionary<string, string>();
                code.Add("code", "373066001");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/ContributoryTobaccoUseVS");
                code.Add("display", "Yes");
                deathRecord.TobaccoUseContributedToDeath = code;
                deathRecord.ActualOrPresumedDateOfDeath = "2018-09-01T00:00:00+06:00";
                deathRecord.DatePronouncedDead = "2018-09-01T00:00:00+04:00";
                deathRecord.DeathFromWorkInjury = true;
                code = new Dictionary<string, string>();
                code.Add("code", "236320001");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/TransportRelationshipsVS");
                code.Add("display", "Vehicle driver");
                deathRecord.DeathFromTransportInjury = code;
                deathRecord.MedicalExaminerContacted = true;
                code = new Dictionary<string, string>();
                code.Add("code", "PHC1260");
                code.Add("system", "http://github.com/nightingaleproject/fhirDeathRecord/sdr/causeOfDeath/vs/PregnancyStatusVS");
                code.Add("display", "Not pregnant within past year");
                deathRecord.TimingOfRecentPregnancyInRelationToDeath = code;
                Dictionary<string, string> detailsOfInjury = new Dictionary<string, string>();
                detailsOfInjury.Add("placeOfInjuryDescription", "Home");
                detailsOfInjury.Add("effectiveDateTime", "2018-04-19T15:43:00+00:00");
                detailsOfInjury.Add("description", "Example details of injury");
                detailsOfInjury.Add("placeOfInjuryLine1", "7 Example Street");
                detailsOfInjury.Add("placeOfInjuryLine2", "Line 2");
                detailsOfInjury.Add("placeOfInjuryCity", "Bedford");
                detailsOfInjury.Add("placeOfInjuryCounty", "Middlesex");
                detailsOfInjury.Add("placeOfInjuryState", "Massachusetts");
                detailsOfInjury.Add("placeOfInjuryZip", "01730");
                detailsOfInjury.Add("placeOfInjuryCountry", "United States");
                detailsOfInjury.Add("placeOfInjuryInsideCityLimits", "true");
                deathRecord.DetailsOfInjury = detailsOfInjury;
                Console.WriteLine(XDocument.Parse(deathRecord.ToXML()).ToString() + "\n\n");
                //Console.WriteLine(FormatJson(deathRecord.ToJSON()) + "\n\n");
                //IJEMortality ije = new IJEMortality(deathRecord);
                //Console.WriteLine(ije.ToString() + "\n");
            }
            else if (args.Length == 2 && args[0] == "ije")
            {
                Console.WriteLine("Converting FHIR SDR to IJE...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                //Console.WriteLine(XDocument.Parse(d.ToXML()).ToString() + "\n");
                IJEMortality ije1 = new IJEMortality(d);
                //Console.WriteLine(ije1.ToString() + "\n\n");
                IJEMortality ije2 = new IJEMortality(ije1.ToString());
                //Console.WriteLine(ije2.ToString() + "\n\n");
                //Console.WriteLine(XDocument.Parse(ije2.ToDeathRecord().ToXML()).ToString() + "\n");
                IJEMortality ije3 = new IJEMortality(new DeathRecord(ije2.ToDeathRecord().ToXML()));
                int issues = 0;
                int total = 0;
                foreach(PropertyInfo property in typeof(IJEMortality).GetProperties())
                {
                    string val1 = Convert.ToString(property.GetValue(ije1, null));
                    string val2 = Convert.ToString(property.GetValue(ije2, null));
                    string val3 = Convert.ToString(property.GetValue(ije3, null));

                    IJEField info = (IJEField)property.GetCustomAttributes().First();

                    if (val1.ToUpper() != val2.ToUpper() || val1.ToUpper() != val3.ToUpper() || val2.ToUpper() != val3.ToUpper())
                    {
                        issues++;
                        Console.WriteLine($"[MISMATCH]\t{info.Name}: {info.Contents} \t\t\"{val1}\" != \"{val2}\" != \"{val3}\"");
                    }
                    total++;
                }
                Console.WriteLine($"\n{issues} issues out of {total} total fields.");
            }
            else if (args.Length == 2 && args[0] == "json2xml")
            {
                Console.WriteLine("Converting FHIR JSON to FHIR XML...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(XDocument.Parse(d.ToXML()).ToString());
            }
            else if (args.Length == 2 && args[0] == "xml2json")
            {
                Console.WriteLine("Converting FHIR XML to FHIR JSON...\n");
                DeathRecord d = new DeathRecord(File.ReadAllText(args[1]));
                Console.WriteLine(d.ToJSON());
            }
            else if (args.Length == 2 && args[0] == "xml2xml")
            {
                // Forces record through getters and then setters, prints as xml
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("GivenNames") || property.Name.Contains("CertifierGivenNames") || property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(XDocument.Parse(outdr.ToXML()).ToString());
            }
            else if (args.Length == 2 && args[0] == "json2json")
            {
                // Forces record through getters and then setters, prints as JSON
                DeathRecord indr = new DeathRecord(File.ReadAllText(args[1]));
                DeathRecord outdr = new DeathRecord();
                List<PropertyInfo> properties = typeof(DeathRecord).GetProperties().ToList();
                foreach(PropertyInfo property in properties)
                {
                    if (property.Name.Contains("GivenNames") || property.Name.Contains("CertifierGivenNames") || property.Name.Contains("CausesOfDeath"))
                    {
                        continue;
                    }
                    property.SetValue(outdr, property.GetValue(indr));
                }
                Console.WriteLine(outdr.ToJSON());
            }
            else if (args.Length == 2 && args[0] == "vrdr")
            {
                DeathRecord vrdr = new DeathRecord();

                // Decedent

                string[] givenNames = {"Robert", "John"};
                vrdr.GivenNames = givenNames;
                vrdr.FamilyName = "Smith";
                vrdr.Suffix = "Jr.";
                vrdr.Nickname = "Bobby";

                vrdr.Gender = "male";

                vrdr.DateOfBirth = "1974-12-31";

                vrdr.SSN = "123-45-6789";

                vrdr.DateOfDeath = "2017-12-31";

                Dictionary<string, string> mscode = new Dictionary<string, string>();
                mscode.Add("code", "S");
                mscode.Add("system", "http://hl7.org/fhir/ValueSet/marital-status");
                mscode.Add("display", "Never Married");
                vrdr.MaritalStatus = mscode;

                Tuple<string, string>[] ethnicity = { Tuple.Create("Hispanic or Latino", "2135-2"), Tuple.Create("Puerto Rican", "2180-8") };
                vrdr.Ethnicity = ethnicity;

                Tuple<string, string>[] race = { Tuple.Create("Native Hawaiian or Other Pacific Islander", "2076-8"), Tuple.Create("Tahitian", "2081-8") };
                vrdr.Race = race;

                Dictionary<string, string> bcode = new Dictionary<string, string>();
                bcode.Add("code", "M");
                bcode.Add("system", "http://hl7.org/fhir/us/core/ValueSet/us-core-birthsex");
                bcode.Add("display", "Male");
                vrdr.BirthSex = bcode;

                Dictionary<string, string> residence = new Dictionary<string, string>();
                residence.Add("residenceLine1", "9 Example Street");
                residence.Add("residenceCity", "Bedford");
                residence.Add("residenceCounty", "Middlesex");
                residence.Add("residenceState", "Massachusetts");
                residence.Add("residenceZip", "01730");
                residence.Add("residenceCountry", "United States");
                residence.Add("residenceInsideCityLimits", "True");
                vrdr.Residence = residence;

                Dictionary<string, string> birthPlace = new Dictionary<string, string>();
                birthPlace.Add("placeOfBirthLine1", "42 Example Street");
                birthPlace.Add("placeOfBirthCity", "Boston");
                birthPlace.Add("placeOfBirthCounty", "Suffolk");
                birthPlace.Add("placeOfBirthState", "Massachusetts");
                birthPlace.Add("placeOfBirthZip", "02101");
                birthPlace.Add("placeOfBirthCountry", "United States");
                vrdr.PlaceOfBirth = birthPlace;


                // Death Pronouncement Performer

                Dictionary<string, string> cqcode = new Dictionary<string, string>();
                cqcode.Add("code", "MD");
                cqcode.Add("system", "http://hl7.org/fhir/v2/0360/2.7");
                cqcode.Add("display", "Doctor of Medicine");
                vrdr.CertifierQualification = cqcode;

                vrdr.CertifierId = "123456789";

                vrdr.CertifierFirstName = "Beverly";
                vrdr.CertifierMiddleName = "Jane";
                vrdr.CertifierFamilyName = "McCoy";


                // Decedent Age

                Dictionary<string, string> age = new Dictionary<string, string>();
                age.Add("value", "70");
                age.Add("unit", "a");
                vrdr.Age = age;


                // Decedent Death Location

                Dictionary<string, string> placeOfDeath = new Dictionary<string, string>();
                placeOfDeath.Add("placeOfDeathTypeCode", "HOSP");
                placeOfDeath.Add("placeOfDeathTypeSystem", "http://hl7.org/fhir/ValueSet/v3-ServiceDeliveryLocationRoleType");
                placeOfDeath.Add("placeOfDeathTypeDisplay", "Hospital");
                placeOfDeath.Add("placeOfDeathPhysicalTypeCode", "wa");
                placeOfDeath.Add("placeOfDeathPhysicalTypeSystem", "http://hl7.org/fhir/ValueSet/location-physical-type");
                placeOfDeath.Add("placeOfDeathPhysicalTypeDisplay", "Ward");
                placeOfDeath.Add("placeOfDeathName", "Example Hospital");
                placeOfDeath.Add("placeOfDeathDescription", "Example Hospital Wing B");
                placeOfDeath.Add("placeOfDeathLine1", "8 Example Street");
                placeOfDeath.Add("placeOfDeathCity", "Bedford");
                placeOfDeath.Add("placeOfDeathCounty", "Middlesex");
                placeOfDeath.Add("placeOfDeathState", "Massachusetts");
                placeOfDeath.Add("placeOfDeathZip", "01730");
                placeOfDeath.Add("placeOfDeathCountry", "United States");
                vrdr.PlaceOfDeath = placeOfDeath;

                // Decedent Death Date

                vrdr.ActualOrPresumedDateOfDeath = "2018-04-24T00:00:00+00:00";
                vrdr.DatePronouncedDead = "2018-04-25T00:00:00+00:00";

                if (args[1] == "json")
                {
                    Console.WriteLine(FormatJson(vrdr.ToJSON()));
                }
                else if (args[1] == "xml")
                {
                    Console.WriteLine(XDocument.Parse(vrdr.ToXML()).ToString());
                }
            }
            else
            {
                foreach (var path in args)
                {
                    ReadFile(path);
                }
            }
        }

        private static void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine($"Reading file '{path}'");
                string contents = File.ReadAllText(path);
                DeathRecord deathRecord = new DeathRecord(contents);

                // Record Information
                Console.WriteLine($"\tRecord ID: {deathRecord.Id}");

                // Decedent Information
                Console.WriteLine($"\tGiven Name: {string.Join(", ", deathRecord.GivenNames)}");
                Console.WriteLine($"\tLast Name: {deathRecord.FamilyName}");
                Console.WriteLine($"\tGender: {deathRecord.Gender}");
                Console.WriteLine($"\tSSN: {deathRecord.SSN}");
                Console.WriteLine($"\tEthnicity: {deathRecord.Ethnicity}");
                Console.WriteLine($"\tDate of Birth: {deathRecord.DateOfBirth}");
                Console.WriteLine($"\tDate of Death: {deathRecord.DateOfDeath}");

                // Certifier Information
                Console.WriteLine($"\tCertifier Given Name: {deathRecord.CertifierGivenNames}");
                Console.WriteLine($"\tCertifier Last Name: {deathRecord.CertifierFamilyName}");
                foreach(var pair in deathRecord.CertifierAddress)
                {
                    Console.WriteLine($"\tCertifierAddress key: {pair.Key}: value: {pair.Value}");
                }
                Console.WriteLine($"\tCertifier Type: {deathRecord.CertifierType}");

                // Conditions
                Tuple<string, string, Dictionary<string, string>>[] causes = deathRecord.CausesOfDeath;
                foreach (var cause in causes)
                {
                    Console.WriteLine($"\tCause: {cause.Item1}, Onset: {cause.Item2}, Code: {string.Join(", ", cause.Item3)}");
                }
                Console.WriteLine($"\tContributing Conditions: {deathRecord.ContributingConditions}");

                // Observations
                Console.WriteLine($"\tAutopsy Performed: {deathRecord.AutopsyPerformed}");
                Console.WriteLine($"\tAutopsy Results Available: {deathRecord.AutopsyResultsAvailable}");
                Console.WriteLine($"\tActual Or Presumed Date of Death: {deathRecord.ActualOrPresumedDateOfDeath}");
                Console.WriteLine($"\tDate Pronounced Dead: {deathRecord.DatePronouncedDead}");
                Console.WriteLine($"\tDeath Resulted from Injury at Work: {deathRecord.DeathFromWorkInjury}");
                Console.WriteLine($"\tDeath From Transport Injury: {string.Join(", ", deathRecord.DeathFromTransportInjury)}");
                Console.WriteLine($"\tDetails of Injury: {string.Join(", ", deathRecord.DetailsOfInjury)}");
                Console.WriteLine($"\tMedical Examiner Contacted: {deathRecord.MedicalExaminerContacted}");
                Console.WriteLine($"\tTiming of Recent Pregnancy In Relation to Death: {string.Join(", ", deathRecord.TimingOfRecentPregnancyInRelationToDeath)}");
                foreach(var pair in deathRecord.MannerOfDeath)
                {
                    Console.WriteLine($"\tManner of Death key: {pair.Key}: value: {pair.Value}");
                }

                foreach(var pair in deathRecord.TobaccoUseContributedToDeath)
                {
                    Console.WriteLine($"\tTobacco Use Contributed to Death key: {pair.Key}: value: {pair.Value}");
                }
            }
            else
            {
                Console.WriteLine($"Error: File '{path}' does not exist");
            }
        }

        private const string INDENT_STRING = "    ";
        static string FormatJson(string json) {

            int indentation = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine +  String.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return String.Concat(result);
        }

    }
}
