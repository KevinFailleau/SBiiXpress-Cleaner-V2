public void TraitementLoop()
        {
            while (Thread.CurrentThread.IsAlive && TraitementDone == false)
            {
                if (cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    ThreadNettoyage();
                    if (!myThreadCleanmgr.IsAlive)
                    {
                        TraitementDone = true;
                        Lb_StatutEnCours.Text = "Arrêté";
                        pB.GetCurrentParent().Invoke((MethodInvoker)delegate { this.pB.Style = ProgressBarStyle.Blocks; this.pB.MarqueeAnimationSpeed = 0; });
                        EnableDisableUI('E');
                        TraitementDone = true;
                        EspaceAp = Ressources.EspaceLibre();
                        MessageBox.Show("Le traitement est maintenant terminé\n" + Ressources.CalculEspaceLibere() + "Mo ont étés libérés sur le disque C:", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        myThreadPrincipal.Abort();
                    }
                }
                else if (!cB_NettoyageWindows.Checked && cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    ThreadCCleaner();

                    if (!myThreadCCleaner.IsAlive)
                    {
                        TraitementDone = true;
                        Lb_StatutEnCours.Text = "Arrêté";
                        pB.GetCurrentParent().Invoke((MethodInvoker)delegate { this.pB.Style = ProgressBarStyle.Blocks; this.pB.MarqueeAnimationSpeed = 0; });
                        EnableDisableUI('E');
                        TraitementDone = true;
                        EspaceAp = Ressources.EspaceLibre();
                        MessageBox.Show("Le traitement est maintenant terminé\n" + Ressources.CalculEspaceLibere() + "Mo ont étés libérés sur le disque C:", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        myThreadPrincipal.Abort();
                    }
                }
                else if (!cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    NettoyageWindowsUpdate();
                }
                else if (!cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    ThreadDownload(10);
                }
                else if (!cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    DesactiverHibernation();
                }
                else if (!cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && cB_IE.Checked)
                {
                    TraitementDone = false;
                    NettoyageIE();
                }
                else if (cB_NettoyageWindows.Checked && cB_CCleaner.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked && !cB_Hibernation.Checked && !cB_IE.Checked)
                {
                    TraitementDone = false;
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadCCleaner();

                    if (!myThreadCCleaner.IsAlive && !myThreadCleanmgr.IsAlive)
                    {
                        TraitementDone = true;
                        Lb_StatutEnCours.Text = "Arrêté";
                        pB.GetCurrentParent().Invoke((MethodInvoker)delegate { this.pB.Style = ProgressBarStyle.Blocks; this.pB.MarqueeAnimationSpeed = 0; });
                        EnableDisableUI('E');
                        TraitementDone = true;
                        EspaceAp = Ressources.EspaceLibre();
                        MessageBox.Show("Le traitement est maintenant terminé\n" + Ressources.CalculEspaceLibere() + "Mo ont étés libérés sur le disque C:", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        myThreadPrincipal.Abort();
                    }
                }
            }
        }