using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ConsoleAppIClock
{
    public struct BusinessDate : IFormattable, IEquatable<BusinessDate>, IComparable<BusinessDate>, IXmlSerializable
    {
        public DateTime CurrentDateTime { get; set; }
        public BusinessDate(DateTime dateTime)
        {
            CurrentDateTime = dateTime;
        }
        /// <summary>
        /// implements method of interface IComparable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] BusinessDate other)
        {
            return CurrentDateTime.CompareTo(other.CurrentDateTime);
        }
        /// <summary>
        /// implements method of interface IEquatable
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] BusinessDate other)
        {
            return CurrentDateTime.Equals(other.CurrentDateTime);
        }
        /// <summary>
        /// implements method of interface IFormattable
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return CurrentDateTime.ToString(format, formatProvider);
        }
        /// <summary>
        /// implements method of interface IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// implements method of interface IXmlSerializable
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            Boolean isEmptyElement = reader.IsEmptyElement; 
            reader.ReadStartElement();
            if (!isEmptyElement) 
            {
                CurrentDateTime = DateTime.ParseExact(reader.
                    ReadElementString("BusinessDate"), "yyyy-MM-dd", null);
                reader.ReadEndElement();
            }
        }
        /// <summary>
        /// implements method of interface IXmlSerializable
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
           if (CurrentDateTime != DateTime.MinValue)
                writer.WriteElementString("BusinessDate",
                    CurrentDateTime.ToString("yyyy-MM-dd"));
        }
    }
}