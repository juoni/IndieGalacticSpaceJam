﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;

using GenericMenuController = PsiEngine.Interface3D.GenericMenuController;
using GameDataInfo          = PsiEngine.GameDataInfo;
using Interface3DButton     = PsiEngine.Input.Interface3dButton;
using ButtonTextures        = PsiEngine.Input.ButtonTextures;




namespace PsiEngineTools
{
	
	
	public class MenuCreator : EditorWindow {
		
		
		
		/*Editor Variables */
		
		//The Editor Window
		private static MenuCreator    _menuCreator    = null;
		private GameObject            _menuController = null;
		private GenericMenuController _GMC            = null;
		
		private string                _menuName       = "NewMenu";
		
		/*******************/
		
		
		
		
		
		
		//Names of the Options
		private string[] _menuOptions            = null;
		
		//Normal,Hover,Armed Are simply Colored Differntly
		private bool  [] _useBackgroundColors    = null;
		
		//Normal,Hover,Armed Use Custom Textures
		private bool  [] _useCustomTextures      = null;
		
		//Textures for the Options if they Use Textures
		private ButtonTextures[] _optionTextures = null;
		
		
		
		private bool[] _hasBackground = null;//Background Layers
		// private bool[] _animations = null;
		
		private Texture2D[] _backgrounds = new Texture2D[1];
		
		
		//Loaded Menu Or new Menu
		//private bool _newMenu          = false;
		private string[] _menuCreatorModes = new string[2] { "New Menu", "LoadMenu" };
		private int _selectedCreatorMode = 0;
		private Vector2[] _menuModesSize = new Vector2[2] { new Vector2(150f, 50f), new Vector2(150f, 50f) };
		/// <summary>
		/// Format [n_textureName][h_textureName][a_textureName]
		/// </summary>
		private bool _autoLoadTextures = false;
		
		//Inner Workings Used when saving a new Menu
		private bool _addingMenuController = false;
		private bool _addingInterface3D    = false;
		private bool _lockCreation = false;
		//==========================================
		[MenuItem("Psi Tools/Menu Creator")]
		static void CreateMenu()
		{
			
			//Initalize Menu Creator Window
			_menuCreator = (MenuCreator)EditorWindow.GetWindow(typeof(MenuCreator));
			
		}
		
		
		void Update()
		{
			
			//Phase One ADD GMC
			if(_menuController != null && _addingMenuController)
			{
				if (!EditorApplication.isCompiling)
				{
					try
					{
						_GMC = (GenericMenuController)UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(_menuController, "Assets/Editor/MenuCreator.cs (94,51)", _menuController.name + GameDataInfo.GetMenuClassBaseSuffix);
					}
					catch (System.Exception ex)
					{
						Debug.Log(ex.ToString());
					}
					
					finally
					{
						if (_GMC != null)
						{
							_addingMenuController = false;
							_addingInterface3D    = true;
						}
					}
				}
			}
			
			
			//Phase two Add Interface3Dbutton
			if (_addingInterface3D && !_lockCreation){
				
				
				int totalOptions = _menuOptions.Length;
				_lockCreation = true;
				Debug.Log("Locking");
				GameObject[] optionObjects = new GameObject[totalOptions];
				UnityEngine.Object parentAsObj = PrefabUtility.InstantiatePrefab(_menuController);
				AssetDatabase.Refresh();
				GameObject parent = (GameObject)parentAsObj;
				
				for(int i = 0; i < totalOptions; ++i)
				{
					
					optionObjects[i]         = new GameObject();
					optionObjects[i].name    = _menuOptions[i];
					
					Interface3DButton   button =   optionObjects[i].AddComponent<Interface3DButton>();
					
					
					if(_useCustomTextures[i])
					{
						if(_optionTextures != null)
						{
							button.textures = _optionTextures[i];
							button.GetComponent<GUITexture>().texture = button.textures.normal;
						}
						else
						{
							EditorUtility.DisplayDialog("Button Textures not found!",
							                            "Option [" + _menuOptions[i] + "]",
							                            "Shit", "fuck");
							Texture2D temp = new Texture2D(32, 32);
							button.GetComponent<GUITexture>().texture = temp;
						}
						
					}
					button.useBackgroundColors = _useBackgroundColors[i];
					
					
					
					
					
					
					optionObjects[i].transform.parent = parent.transform;
					
					
					
					//try
					//{
					//    if (_useCustomTextures[i])
					//    {
					//        button.textures.UseTextures(true);
					//        but
					//    }
					//}
					//catch (System.Exception ex)
					//{
					//    Debug.Log("Couldn't Add Textures Try again later?" + ex.ToString());
					//}
				}
				_addingInterface3D = false;
				_lockCreation = false;
				
				
				PsiEngine.GameDataInfo.CreateNewPrefab(parent, PsiEngine.GameDataInfo.GetMenuPrefabPath(parent.name));
			}
		}
		
		void OnGUI()
		{
			
			
			_selectedCreatorMode = GUILayout.SelectionGrid(_selectedCreatorMode, _menuCreatorModes, 2,GUILayout.Height(_menuModesSize[0].y),
			                                               GUILayout.Width(_menuModesSize[0].x),GUILayout.Width(_menuModesSize[1].x),GUILayout.Height(_menuModesSize[1].y));
			
			
			
			//New Menu Related
			GUILayout.Label("Menu Name");
			_menuName = GUILayout.TextArea(_menuName,GUILayout.Height(15f),GUILayout.Width(150f));
			
			_autoLoadTextures = GUILayout.Toggle(_autoLoadTextures, "Auto Load Textures", GUILayout.Width(150f), GUILayout.Height(25f));
			
			
			
			#region Universial Options
			
			
			#endregion Universal Options
			
			
			if(GUILayout.Button("Create Menu",GUILayout.Width(150f),GUILayout.Height(25f)))
			{
				
				bool userWantsToSave = EditorApplication.SaveCurrentSceneIfUserWantsTo();
				
				
				EditorApplication.NewScene();
				bool newSceneSaved =  EditorApplication.SaveScene( PsiEngine.GameDataInfo.GetMenuPath +
				                                                  "/" + _menuName +".unity");
				
				
				//PsiEngine.GenericMenuController GMC = (PsiEngine.GenericMenuController) PsiEngine.GameDataInfo.GenerateGenericMenu(_menuName, true, _menuOptions).AddComponent(_menuName + "Controller");
				//GMC.CloseWindow(GMC.gameObject);
				if (EditorUtility.DisplayDialog("Create Menu?", "Are you sure you want to create this menu?", "Create", "Cancel"))
				{
					_menuController = PsiEngine.GameDataInfo.GenerateGenericMenu(_menuName, true, _menuOptions);
					if (_menuController)
					{
						
						_addingMenuController = true;
					}
				}
				else { Debug.Log("DID NOT CREATE MENU: [" + _menuName + "] User Declined"); }
				
			}
			
			
			#region Add Option
			if (GUILayout.Button("Add Option", GUILayout.Width(150f), GUILayout.Height(25f)))
			{
				if(_menuOptions == null){
					InitilizeOptions();
				}else{
					AddNewOption(); 
				}
			}
			#endregion
			
			try
			{
				
				for (int i = 0; i < _menuOptions.Length; ++i)
				{
					EditorGUILayout.PrefixLabel("Option Name");
					
					
					
					
					
					
					GUILayout.BeginHorizontal();
					
					if (GUILayout.Button("Remove " + _menuOptions[i], GUILayout.Width(150f), GUILayout.Height(25f)))
					{
						//Remove Option i
						RemoveOption(i);
					}
					
					//Text Field to Edit Name
					//Don't allow for Special characters
					_menuOptions[i] = EditorGUILayout.TextField(_menuOptions[i], GUILayout.Width(150f), GUILayout.Height(25f));
					
					ParseSpecialCharacters(i);
					
					GUILayout.EndHorizontal();
					
					_useBackgroundColors[i] = GUILayout.Toggle(_useBackgroundColors[i], "ColorMode");
					_useCustomTextures[i] = GUILayout.Toggle(_useCustomTextures[i], "TextureMode");
					
					
					EditorGUILayout.BeginHorizontal();
					
					if(_useCustomTextures[i])
					{
						Texture2D[] texturesToCompare;
						
						if (_autoLoadTextures)
						{
							texturesToCompare = new Texture2D[3] { _optionTextures[i].normal, _optionTextures[i].hover, _optionTextures[i].armed };
							
							GUILayout.Label("Normal");
							_optionTextures[i].normal = EditorGUILayout.ObjectField("", _optionTextures[i].normal, typeof(Texture2D),
							                                                        GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
							try
							{
								if (_optionTextures[i].normal.Equals(texturesToCompare[0]))
								{
									
									AutoLoadTextures(ref _optionTextures[i],i);
									
									
									
								}
							}
							catch
							{
								
							}
							GUILayout.Label("Hover");
							_optionTextures[i].hover = EditorGUILayout.ObjectField("", _optionTextures[i].hover, typeof(Texture2D),
							                                                       GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
							
							GUILayout.Label("Armed");
							_optionTextures[i].armed = EditorGUILayout.ObjectField("", _optionTextures[i].armed, typeof(Texture2D),
							                                                       GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
						}
						
						else
						{
							GUILayout.Label("Normal");
							_optionTextures[i].normal = EditorGUILayout.ObjectField("", _optionTextures[i].normal, typeof(Texture2D),
							                                                        GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
							
							
							
							GUILayout.Label("Hover");
							_optionTextures[i].hover = EditorGUILayout.ObjectField("", _optionTextures[i].hover, typeof(Texture2D),
							                                                       GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
							
							GUILayout.Label("Armed");
							_optionTextures[i].armed = EditorGUILayout.ObjectField("", _optionTextures[i].armed, typeof(Texture2D),
							                                                       GUILayout.Height(25f), GUILayout.Width(25f)) as Texture2D;
						}
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			catch(Exception ex)
			{
				
				Debug.Log(ex.ToString());
			}
			if (GUILayout.Button("CleanUP", GUILayout.Width(150f), GUILayout.Height(75f)))
			{
				OnFinished();
			}
			
		}
		
		private void AutoLoadTextures(ref ButtonTextures bt, int optionIndex)
		{
			if( bt.ButtonTextureExists(PsiEngine.Input.ButtonState.normal))
			{
				int lastIndex     = AssetDatabase.GetAssetPath(bt.normal.GetInstanceID()).LastIndexOf('/');
				
				string actualPath = AssetDatabase.GetAssetPath(bt.normal.GetInstanceID()).Remove(lastIndex);
				
				bt.hover = (Texture2D)AssetDatabase.LoadAssetAtPath(actualPath + "/h_" + _menuOptions[optionIndex] + ".png",typeof(Texture2D));
				bt.armed = (Texture2D)AssetDatabase.LoadAssetAtPath(actualPath + "/a_" + _menuOptions[optionIndex] + ".png", typeof(Texture2D));
			}
		}
		
		
		private void OnFinished()
		{
			_GMC = null;
			_menuController = null;
			_useBackgroundColors = null;
			_hasBackground = null;
			_useCustomTextures = null;
			_menuOptions = null;
			_optionTextures = null;
			
		}
		
		
		
		
		
		private void InitilizeOptions()
		{
			_menuOptions = new string[0];
			
			
			_useBackgroundColors = new bool[0];
			_hasBackground = new bool[0];
			_useCustomTextures = new bool[0];
			_optionTextures = new ButtonTextures[0];
			
			ArrayUtility.Add<string>(ref _menuOptions, "Option" + _menuOptions.Length.ToString());
			ArrayUtility.Add<bool>(ref _useBackgroundColors, false);
			ArrayUtility.Add<bool>(ref _useCustomTextures, false);
			ArrayUtility.Add<ButtonTextures>(ref _optionTextures, new ButtonTextures());
		}
		
		
		//Set up Option Defaults
		private void AddNewOption()
		{
			ArrayUtility.Add<string>(ref _menuOptions, "Option" + _menuOptions.Length.ToString());
			ArrayUtility.Add<bool>(ref _useBackgroundColors, false);
			ArrayUtility.Add<bool>(ref _useCustomTextures, false);
			ArrayUtility.Add<ButtonTextures>(ref _optionTextures,new ButtonTextures());
		}
		
		//remove Option From 
		private void RemoveOption(int option)
		{
			ArrayUtility.RemoveAt<string>(ref _menuOptions, option);
			ArrayUtility.RemoveAt<bool>(ref _useBackgroundColors, option);
			ArrayUtility.RemoveAt<bool>(ref _useBackgroundColors, option);
			
		}
		
		
		private void ParseSpecialCharacters(int option)
		{
			
			if (_menuOptions[option].Contains(" "))
			{
				_menuOptions[option] = _menuOptions[option].Replace(' ', '_');
			}
		}
		
		
		
	}
	
	
}

namespace PsiEngine
{
	using System.IO;
	public static class GameDataInfo
	{
		
		
		private const string MENU_PATH = "Assets/Scenes/Menus";
		private const string MENU_PREFAB_PATH = "Assets/MenuPrefabs/Resources";
		private const string MENU_CLASS_BASESUFFIX = "Controller";
		
		private const string PSIMATION_PATH = "Assets/ChaosComplex/Resources/Animations";
		
		
		
		
		private static bool EnsurePathExists(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				AssetDatabase.Refresh();
			}
			
			return Directory.Exists(path) ? true : false;
		}
		
		public static GameObject  GenerateGenericMenu(string name, bool hasAnimations ,string[] options)
		{
			
			
			//Creates the Path where our objects will go
			string path = System.IO.Path.Combine(MENU_PREFAB_PATH, name);
			
			
			
			
			//Creat Path if it doesn't exist
			EnsurePathExists(path);
			
			
			//Name File, Make it a C# class
			//create the file
			string filename = name + "Controller";
			string filetype = ".cs";
			string newPath = System.IO.Path.Combine(path, filename + filetype);
			
			
			//If the File Doesn't Already Exist Generate the code
			if(!System.IO.File.Exists(newPath))
			{
				string inherit = hasAnimations ? " : AnimatedGenericMenuController" : " : GenericMenuController";
				
				string newLine = System.Environment.NewLine;
				
				
				
				string generatedCode = "using UnityEngine;" + newLine +
					"using PsiEngine.@Input;" + newLine +
						"using PsiEngine.Interface3D;" + newLine+
						newLine + newLine + newLine +
						"public class " + filename + inherit +
						"{" + newLine +
						"void Awake(){}" +
						newLine + newLine + newLine +
						"void Start(){" + newLine + newLine + newLine + " try{" + newLine + "InitilizeOptions();" + newLine + "}" + newLine + " catch(System.Exception ex)" + newLine + "{" + newLine +
						"Debug.Log(ex.ToString());}" + newLine + "}";
				//   Add Options
				foreach( string option in options)
				{
					generatedCode += newLine +
						"public virtual void " + option + "(){}";
				}
				//    End Script
				generatedCode += "\n}";
				
				//string generatedCode =
				//@"using UnityEngine;
				//using PsiEngine.@Input;
				//
				//public class " + filename + inherit + @"
				//{
				//    void Awake(){}
				//
				//    void Start()
				//    {
				//        try
				//        {
				//            InitilizeOptions();
				//        }
				//        catch(System.Exception ex)
				//        {
				//            Debug.Log(ex.ToString());
				//        }
				//    }
				//    " +
				//      string.Join("", options.Select(option => @"
				//    public virtual void " + option + @"()
				//    {}
				//    ").ToArray()) + @"
				//}
				//    
				//";
				
				
				System.IO.File.WriteAllText(newPath,generatedCode);
			}
			
			AssetDatabase.Refresh();
			
			
			//Create Main Prefab and Refresh
			// GameObject menu =  
			
			
			return PrefabUtility.CreatePrefab(MENU_PREFAB_PATH+"/"+name+"/"+name+".prefab", Camera.main.gameObject);
			
			
			
		}
		
		
		public static string GetMenuPath
		{
			
			get 
			{
				
				if (EnsurePathExists(MENU_PATH))
					return MENU_PATH;
				else
				{
					if (Application.isEditor)
					{
						Debug.Log("Path not created");
					}
					return string.Empty;
				}
			}
			
		}
		
		public static string GetPsiMationPath
		{
			get { return PSIMATION_PATH; }
		}
		public static string GetMenuPrefabPath(string menuName)
		{
			
			string path = MENU_PREFAB_PATH + "/" + menuName + "/" + menuName + ".prefab";
			return path;
		}
		public static string GetMenuClassBaseSuffix
		{
			get { return MENU_CLASS_BASESUFFIX; }
		}
		
		
		public static GameObject GetGenericMenuPrefab(string name)
		{
			GameObject genericPrefab = new GameObject();
			
			EnsurePathExists(MENU_PREFAB_PATH + "/" + name + "/" + name + ".cs");
			return genericPrefab;
		}
		
		public static bool GenerateMenuClassFile(string path )
		{
			return true;    
		}
		
		
		public static void AddComponents(GameObject go, string[] components)
		{
			
		}
		
		public static void CreateNewPrefab(GameObject obj, string localPath)
		{
			UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
			PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
			
		}
		
		public  static string[] GetFullAnimationAsPaths()
		{
			throw new NotImplementedException();
		}
	}
	
	
}
