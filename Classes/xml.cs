using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace runnerDotNet
{
    public class xml : XClass
    {
        /// <summary>
        /// Converts the XML text that was generated by this class to an array.
	    /// It can work with unidimensional and multidimensional associative arrays.
        /// </summary>
        /// <param name="arg_str_xml">The xml text to be changed into an array</param>
        /// <returns></returns>
        public XVar xml_to_array(XVar arg_str_xml)
        {
			try
			{
				XDocument xDoc = XDocument.Parse(arg_str_xml);
				return prv_xml_to_array(xDoc.Root);
			}
			catch (Exception ex)
			{
				return null;
			}
			
            /*		    $parser = xml_parser_create("UTF-8");
		
		    xml_parse_into_struct($parser, $arg_str_xml, $arr_raw_xml);
		    $arr_out = array();
		    $this->prv_xml_to_array($arr_raw_xml, $arr_out);
		    return $arr_out;*/
        }

        /// <summary>
        /// Converts xml to array recursively
        /// </summary>
        /// <param name="arg_tags">the raw array of tags got from xml_parse_into_struct is passed by reference to keep the position of the pointer in the array through function calls.</param>
        /// <param name="arg_current_tag">arg_current_tag the current array to be filled is passed by reference because it is changed within the function.</param>
        private XVar prv_xml_to_array(XElement xNode)
        {
            XVar result = XVar.Array();
            foreach (var xElement in xNode.Elements())
	        {
                if (xElement.HasElements)
                {
                    result[xElement.Attribute("value").Value] = this.prv_xml_to_array(xElement);
                }
                else
                {
                    result[xElement.Attribute("value").Value] = xElement.Value;
                }
	        }
            return result;
        }

        /// <summary>
        /// Sets padding in xml text.
	    /// Helps to make the xmlcode readable but can be disabled by emptying arg_str_pad when we do not need to read the xml code.
        /// </summary>
        /// <param name="arg_int_pad_number">the number of indentation pads in this tag</param>
        /// <param name="arg_str_pad">the single pad size</param>
        /// <returns></returns>
        private XVar pad(XVar arg_int_pad_number = null, XVar arg_str_pad = null)
        {
            if (arg_int_pad_number as Object == null || arg_str_pad as Object == null)
            {
                return null;
            }

		    StringBuilder str_pad = new StringBuilder();
            for(int i = 0; i < arg_int_pad_number.Count(); i++)
            {
		        str_pad.Append(arg_str_pad.ToString());
		    }
    		return str_pad.Length > 0  ? "\n" + str_pad.ToString() : "";
        }

        /// <summary>
        /// Changes php arrays into xml text recursively
        /// </summary>
        /// <param name="arg_arr_array">the array to be changed into XML</param>
        /// <param name="arg_int_pad_number">the number of pads of the current tag</param>
        /// <param name="arg_str_pad">the indentation pad text</param>
        /// <returns>xml text</returns>
        private object[] prv_array_to_xml(XVar arg_arr_array)
        {
            List<XElement> elements = new List<XElement>();
            foreach (var item in arg_arr_array.GetEnumerator())
            {
                XElement xElement = new XElement("attr");
				xElement.Add(new XAttribute("value", item.Key.ToString()));
                if ((item.Value as XVar).IsArray())
                {
                    xElement.Add(prv_array_to_xml(item.Value));
                }
                else
                {
                    if (item.Value is bool || item.Value is XVar && item.Value.Value is bool)
						xElement.Value = ((bool)item.Value) ? "true" : "false";
					else xElement.Value = item.Value;
                }
                elements.Add(xElement);
            }
            return elements.ToArray();
        }

        /// <summary>
        /// changes php arrays into xml text recursively
        /// </summary>
        /// <param name="arg_arr_array">the array to be changed into XML</param>
        /// <param name="_arg_str_operation_name">the name of the main xml tag</param>
        /// <param name="arg_str_pad">the indentation pad text</param>
        /// <returns>xml text</returns>
        public XVar array_to_xml(XVar arg_arr_array, XVar _arg_str_operation_name = null, XVar arg_str_pad = null)
        {
            if (arg_arr_array as Object == null || !arg_arr_array.IsArray())
            {
                return false;
            }
            XVar arg_str_operation_name = _arg_str_operation_name ?? "report";
            XDocument xDoc = new XDocument();
            xDoc.Add(new XElement(arg_str_operation_name.ToString()));
            xDoc.Root.Add(this.prv_array_to_xml(arg_arr_array));
            return xDoc.ToString();
        }
    
    }
}