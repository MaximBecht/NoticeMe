using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using static System.Net.WebRequestMethods;

namespace NoticeMe.Data
{
    public class ObjectSerializer
    {
        public async static Task<bool> SerializeAsync<T>(StorageFile storageFile, T objectToSave)
        {
            if (storageFile != null)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));
                    using (Stream fileStream = await storageFile.OpenStreamForWriteAsync().ConfigureAwait(false))
                    {
#if DEBUG
                        using (var xmlWriter = XmlWriter.Create(fileStream, new XmlWriterSettings { Indent = true }))
                        {
                            serializer.Serialize(xmlWriter, objectToSave);
                            return true;
                        }
#else
                        serializer.Serialize(fileStream, objectToSave);
                        return true;
#endif
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }
        public async static Task<T> DeserializeAsync<T>(StorageFile storageFile)
        {
            if(storageFile != null)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));
                    using (Stream fileStream = await storageFile.OpenStreamForWriteAsync().ConfigureAwait(false))
                    {
                        return (T)serializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return default(T);
        }

        public async static Task<bool> SerializeToInternalAsync<T>(string filenName, T objectToSave)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(filenName, CreationCollisionOption.ReplaceExisting);

            return await SerializeAsync<T>(file, objectToSave);
        }
        public async static Task<T> DeserializeFromInternalAsync<T>(string filenName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await storageFolder.GetFileAsync(filenName);
                return await DeserializeAsync<T>(file);
            }
            catch(Exception ex)
            {

            }
            return default(T);
        }
    }
}
