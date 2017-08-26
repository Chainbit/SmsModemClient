makecert.exe -r -pe -sky exchange -n "CN=ClientCert" ClientCert.cer -sv ClientCert.pvk
pvk2pfx.exe -pvk ClientCert.pvk -spc ClientCert.cer -pfx ClientCert.pfx
makecert.exe -r -pe -sky exchange -n "CN=ServerCert" ServerCert.cer -sv ServerCert.pvk
pvk2pfx.exe -pvk ServerCert.pvk -spc ServerCert.cer -pfx ServerCert.pfx