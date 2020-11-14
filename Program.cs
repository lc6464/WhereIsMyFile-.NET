using System;
using System.IO;
using System.Media;
using System.Drawing;
using WhereIsMyFile.Properties;
using Message;
using HideWindow;

namespace WhereIsMyFile {
	class Program {
		static void Main(string[] args) {
			HideConsole.Hide();

			try {
				Icon LC = Resources.LC;
				FileStream icon = File.Create(@"icon.ico");
				LC.Save(icon);
				LC.Dispose();
				icon.Dispose();
				new FileInfo(@"icon.ico") {
					Attributes = FileAttributes.System | FileAttributes.Hidden
				};
			} catch (Exception e) {
				Console.WriteLine(e);
			}

			try {
				byte[] ini = Resources.desktop_ini;
				FileStream fileStream = File.Create(@"desktop.ini");
				fileStream.Write(ini, 0, ini.Length);
				fileStream.Dispose();
				new FileInfo(@"desktop.ini") {
					Attributes = FileAttributes.System | FileAttributes.Hidden
				};
			} catch (Exception e) {
				Console.WriteLine(e);
			}

			try {
				byte[] wave = new byte[Resources.Information.Length];
				Resources.Information.Read(wave, 0, wave.Length);
				FileStream file = File.Create($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\music.wav");
				file.Write(wave, 0, wave.Length);
				file.Dispose();
			} catch (Exception e) {
				Console.WriteLine(e);
			}
			
			SoundPlayer player = new SoundPlayer(Resources.Information);
			player.Load(); player.Play();
			
			if (args.Length > 0) {
				Box.Warning($"无法找到文件“{args[0]}”！", "WPS Office", Buttons.OK);
			} else {
				Box.Warning("无法找到此文件！", "WPS Office", Buttons.OK);
			}
		}
	}
}
