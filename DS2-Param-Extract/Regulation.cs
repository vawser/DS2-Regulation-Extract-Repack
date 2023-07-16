using SoulsFormats.Util;
using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2_Param_Extract
{
    public class Regulation
    {
        public string Path_Regulation;
        public string Path_Param_Folder;
        public string Path_Edit_Folder;
        public string Path_PARAMDEF = AppContext.BaseDirectory + "\\Assets\\Paramdex\\DS2S\\Defs\\";

        public List<PARAMDEF> PARAMDEF_List = new List<PARAMDEF>();

        public IBinder? regulationBinder { get; set; }
        public bool isRegulationEncrypted { get; set; }
        public bool usingRegulation { get; set; }
        public List<ParamWrapper> regulationParamWrappers { get; set; }

        public bool includeEMEVD = false;
        public bool includeFMG = false;

        public string Path_BaseMod;

        public Regulation(string basePath, bool extractEMEVD, bool extractFMG)
        {
            regulationParamWrappers = new List<ParamWrapper>();

            Path_BaseMod = basePath;

            Path_Regulation = Path.Combine(Path_BaseMod, "enc_regulation.bnd.dcx");
            Path_Param_Folder = Path.Combine(Path_BaseMod, "Param");
            Path_Edit_Folder = Path.Combine(Path_BaseMod, "Edit");

            usingRegulation = true;
            includeEMEVD = extractEMEVD;
            includeFMG = extractFMG;

            if(!Directory.Exists(Path_Param_Folder))
            {
                Directory.CreateDirectory(Path_Param_Folder);
            }

            if (!Directory.Exists(Path_Edit_Folder))
            {
                Directory.CreateDirectory(Path_Edit_Folder);
            }

            if (!File.Exists(Path_Regulation))
            {
                Util.ShowError($"Regulation not found:\r\n{Path_Regulation}");
                usingRegulation = false;
            }

            BuildParamDefs();
        }

        public void BuildParamDefs()
        {
            PARAMDEF_List.Clear();
            var paramdef_dir = $@"Assets\\Paramdex\\DS2S\\Defs";

            foreach (string path in Directory.GetFiles(Path_PARAMDEF, "*.xml"))
            {
                string paramID = Path.GetFileNameWithoutExtension(path);

                try
                {
                    var paramdef = PARAMDEF.XmlDeserialize(path);

                    PARAMDEF_List.Add(paramdef);
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to load paramdef {paramID}.txt\r\n\r\n{ex}");
                }
            }
        }

        public bool LoadParams()
        {
            // Load regulation params (if they exist)
            if (usingRegulation)
            {
                try
                {
                    regulationBinder = SFUtil.DecryptDS2Regulation(Path_Regulation);
                    isRegulationEncrypted = true;
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to load regulation:\r\n{Path_Regulation}\r\n\r\n{ex}");

                    return false;
                }

                foreach (BinderFile file in regulationBinder.Files.Where(f => f.Name.EndsWith(".param")))
                {
                    string name = Path.GetFileNameWithoutExtension(file.Name);

                    try
                    {
                        PARAM param = PARAM.Read(file.Bytes);

                        foreach (PARAMDEF paramdef in PARAMDEF_List)
                        {
                            if (param.ParamType == paramdef.ParamType)
                                param.ApplyParamdef(paramdef);
                        }

                        var wrapper = new ParamWrapper(name, param, param.AppliedParamdef, false);
                        regulationParamWrappers.Add(wrapper);

                        //MessageBox.Show($"Load Regulation: {wrapper.Name}");
                    }
                    catch (Exception ex)
                    {
                        Util.ShowError($"Failed to load param file: {name}.param\r\n\r\n{ex}");
                    }
                }
            }

            regulationParamWrappers.Sort();

            return true;
        }

        public bool LoadLooseParams()
        {
            string[] paramFiles = Directory.GetFileSystemEntries(Path_Param_Folder, @"*.param");
            foreach (string filename in paramFiles)
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                var paramBytes = File.ReadAllBytes(filename);

                try
                {
                    PARAM param = PARAM.Read(paramBytes);

                    foreach (PARAMDEF paramdef in PARAMDEF_List)
                    {
                        if (param.ParamType == paramdef.ParamType)
                            param.ApplyParamdef(paramdef);
                    }

                    var wrapper = new ParamWrapper(name, param, param.AppliedParamdef, true);
                    regulationParamWrappers.Add(wrapper);

                    //MessageBox.Show($"Load Loose: {wrapper.Name}");
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to load param file: {name}.param\r\n\r\n{ex}");
                }
            }

            return true;
        }

        public bool SaveParams()
        {
            // Save regulation params
            if (usingRegulation)
            {
                try
                {
                    foreach (BinderFile file in regulationBinder.Files)
                    {
                        foreach (ParamWrapper wrapper in regulationParamWrappers)
                        {
                            ParamWrapper paramFile = wrapper;

                            // Only save those that were in the regulation during load back into the regulation
                            if (!paramFile.isLoose)
                            {
                                if (Path.GetFileNameWithoutExtension(file.Name) == paramFile.Name)
                                {
                                    try
                                    {
                                        file.Bytes = paramFile.Param.Write();
                                        //MessageBox.Show($"{file.Name}");
                                    }
                                    catch
                                    {
                                        Util.ShowError($"Invalid data, failed to save {paramFile}. Data must be fixed before saving can complete.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowError($"Failed to save regulation file:\n{Path_Regulation}\n\n{ex}");
                    return false;
                }
            }

            if (regulationBinder is BND4 bnd4)
            {
                bnd4.Write(Path_Regulation);
            }

            return true;
        }

        public bool SaveLooseParams()
        {
            string[] paramFiles = Directory.GetFileSystemEntries(Path_Param_Folder, @"*.param");
            foreach (string filename in paramFiles)
            {
                string name = Path.GetFileNameWithoutExtension(filename);

                foreach (ParamWrapper wrapper in regulationParamWrappers)
                {
                    ParamWrapper paramFile = wrapper;

                    // Only save those that were in the regulation during load back into the regulation
                    if (paramFile.isLoose)
                    {
                        if (Path.GetFileNameWithoutExtension(name) == paramFile.Name)
                        {
                            try
                            {
                                var paramBytes = paramFile.Param.Write();
                                File.WriteAllBytes(filename, paramBytes);
                            }
                            catch
                            {
                                Util.ShowError($"Invalid data, failed to save {paramFile}. Data must be fixed before saving can complete.");
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool SaveAllParamsAsLoose()
        {
            // Save params from regulation
            foreach (BinderFile file in regulationBinder.Files)
            {
                string name = Path.GetFileNameWithoutExtension(file.Name);

                foreach (ParamWrapper wrapper in regulationParamWrappers)
                {
                    ParamWrapper paramFile = wrapper;

                    if (Path.GetFileNameWithoutExtension(name) == paramFile.Name)
                    {
                        try
                        {
                            var param_path = Path.Combine(Path_Param_Folder, file.Name);
                            var paramBytes = paramFile.Param.Write();
                            File.WriteAllBytes(param_path, paramBytes);
                        }
                        catch
                        {
                            Util.ShowError($"Invalid data, failed to save {paramFile}. Data must be fixed before saving can complete.");
                            return false;
                        }
                    }
                }
            }

            // Save loose params
            string[] paramFiles = Directory.GetFileSystemEntries(Path_Param_Folder, @"*.param");
            foreach (string filename in paramFiles)
            {
                string name = Path.GetFileNameWithoutExtension(filename);

                foreach (ParamWrapper wrapper in regulationParamWrappers)
                {
                    ParamWrapper paramFile = wrapper;

                    if (Path.GetFileNameWithoutExtension(name) == paramFile.Name)
                    {
                        try
                        {
                            var param_path = Path.Combine(Path_Param_Folder, filename);
                            var paramBytes = paramFile.Param.Write();
                            File.WriteAllBytes(param_path, paramBytes);
                        }
                        catch
                        {
                            Util.ShowError($"Invalid data, failed to save {paramFile}. Data must be fixed before saving can complete.");
                            return false;
                        }
                    }
                }
            }

            if(includeEMEVD)
            {
                foreach (var p in regulationBinder.Files)
                {
                    if (p.Name.ToUpper().Contains(".EMEVD"))
                    {
                        File.WriteAllBytes(Path.Combine(Path_Edit_Folder, p.Name), p.Bytes);
                    }
                }
            }

            if(includeFMG)
            {
                foreach (var p in regulationBinder.Files)
                {
                    if (p.Name.ToUpper().Contains(".FMG"))
                    {
                        File.WriteAllBytes(Path.Combine(Path_Edit_Folder, p.Name), p.Bytes);
                    }
                }
            }

            // Empty regulation of params
            List<BinderFile> newFiles = new List<BinderFile>();
          
            foreach (var p in regulationBinder.Files)
            {
                if (!p.Name.ToUpper().Contains(".PARAM"))
                {
                    Console.WriteLine($"{p.Name} - {p.ToString()}");

                    if (!includeEMEVD)
                    {
                        if (!p.Name.ToUpper().Contains(".EMEVD"))
                        {
                            newFiles.Add(p);
                        }
                    }
                    if (!includeFMG)
                    {
                        if (!p.Name.ToUpper().Contains(".FMG"))
                        {
                            newFiles.Add(p);
                        }
                    }
                }
            }

            regulationBinder.Files = newFiles;

            if (regulationBinder is BND4 bnd4)
            {
                bnd4.Write(Path_Regulation);
            }

            return true;
        }

        public bool RepackFiles()
        {
            try
            {
                regulationBinder = SFUtil.DecryptDS2Regulation(Path_Regulation);
                isRegulationEncrypted = true;
            }
            catch (Exception ex)
            {
                Util.ShowError($"Failed to load regulation:\r\n{Path_Regulation}\r\n\r\n{ex}");

                return false;
            }

            List<BinderFile> newFiles = new List<BinderFile>();

            string[] emevdFiles = Directory.GetFileSystemEntries(Path_Edit_Folder, @"*.emevd");
            foreach (string filename in emevdFiles)
            {
                string name = Path.GetFileNameWithoutExtension(filename);

                BinderFile newFile = new BinderFile(Binder.FileFlags.Flag1, File.ReadAllBytes(filename));
                newFile.Name = $"{name}.emevd";
                newFile.ID = -1;

                newFiles.Add(newFile);
            }

            string[] fmgFiles = Directory.GetFileSystemEntries(Path_Edit_Folder, @"*.fmg");
            foreach (string filename in fmgFiles)
            {
                string name = Path.GetFileNameWithoutExtension(filename);

                BinderFile newFile = new BinderFile(Binder.FileFlags.Flag1, File.ReadAllBytes(filename));
                newFile.Name = $"{name}.fmg";
                newFile.ID = -1;

                newFiles.Add(newFile);
            }

            regulationBinder.Files = newFiles;

            if (regulationBinder is BND4 bnd4)
            {
                bnd4.Write(Path_Regulation);
            }

            return true;
        }
    }
}
