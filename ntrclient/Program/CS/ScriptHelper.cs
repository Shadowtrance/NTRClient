﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntrclient {
	public class ScriptHelper {
        public void bpadd(uint addr, string type = "code.once") {
            uint num = 0;
            if (type == "code") {
                num = 1;
            }
            if (type == "code.once") {
                num = 2;
            }
            if (num != 0) {
                Program.ntrClient.sendEmptyPacket(11, num, addr, 1);
            }
        }

        public void bpdis(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 3);
        }

        public void bpena(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 2);
        }

        public void resume() {
            Program.ntrClient.sendEmptyPacket(11, 0, 0, 4);
        }



		public void connect(string host, int port) {
			Program.ntrClient.setServer(host, port);
			Program.ntrClient.connectToServer();
		}

		public void reload() {
			Program.ntrClient.sendReloadPacket();
		}
		
		public void listprocess() {
			Program.ntrClient.sendEmptyPacket(5);
		}

		public void listthread(int pid) {
			Program.ntrClient.sendEmptyPacket(7, (uint) pid);
		}

		public void attachprocess(int pid, uint patchAddr = 0) {
			Program.ntrClient.sendEmptyPacket(6, (uint) pid, patchAddr);
		}

        public void queryhandle(int pid) {
            Program.ntrClient.sendEmptyPacket(12, (uint)pid );
        }

		public void memlayout(int pid) {
			Program.ntrClient.sendEmptyPacket(8, (uint)pid);
		}

		public void disconnect() {
			Program.ntrClient.disconnect();
		}

		public void sayhello() {
			Program.ntrClient.sendHelloPacket();
		}

		public void data(uint addr, uint size = 0x100, int pid = -1, string filename = null) {
			if (filename == null && size > 1024) {
				size = 1024;
            }
            Program.ntrClient.sendReadMemPacket(addr, size, (uint)pid, filename);
        }

        public void write(uint addr, byte[] buf, int pid = -1)
        {
            Program.ntrClient.sendWriteMemPacket(addr, (uint)pid, buf);
        }

        public void write(uint addr, byte buf, int pid = -1)
        {
            byte[] temp = { buf };
            Program.ntrClient.sendWriteMemPacket(addr, (uint)pid, temp);
        }

        public void sendfile(String localPath, String remotePath) {
			FileStream fs = new FileStream(localPath, FileMode.Open);
			byte[] buf = new byte[fs.Length];
			fs.Read(buf, 0, buf.Length);
			fs.Close();
			Program.ntrClient.sendSaveFilePacket(remotePath, buf);
		}

        public void read(uint addr, uint size = 4, int pid = -1)
        {
            data(addr, size, pid, null);
        }

        public void remoteplay()
        {
            Program.ntrClient.sendEmptyPacket(901);
            Program.ntrClient.log("Disconnecting in 10 seconds to improve performance");
            Program.gCmdWindow.timer2.Enabled = true;
        }

        public void debug()
        {
            Program.dc.Show();
        }
	}
}
