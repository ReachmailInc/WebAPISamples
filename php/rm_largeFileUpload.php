<?php
/**
Large File Upload
*/
                        function rm_largeFileUpload($account_key, $username, $password, $file) {
                                                $upload_data_url = 'https://services.reachmail.net/Rest/Data/';
                                                $header = array("Content-Type: application/xml");
                                                $fp = file_get_contents($file);
                                                $request_body = $fp;
                                                $upload_file_request = curl_init();
                                                $curl_options = array(
                                                                CURLOPT_URL => $upload_data_url,
                                                                CURLOPT_HEADER => false,
                                                                CURLOPT_USERPWD => "$account_key\\$username:$password",
                                                                CURLOPT_HTTPHEADER => $header,
                                                                CURLOPT_FOLLOWLOCATION => true,
                                                                CURLOPT_POST => true,
                                                                CURLOPT_POSTFIELDS => $request_body,
                                                                CURLOPT_RETURNTRANSFER => true
                                                );
                                                curl_setopt_array($upload_file_request, $curl_options);
                                                $upload_file_response = curl_exec($upload_file_request);
                                                curl_close($upload_file_request);
                                                $xml = simplexml_load_string($upload_file_response);
                                                $upload_id = $xml->Id;
                                                print "\nYour file has been successfully uploaded!\nYour upload id: $upload_id\n\n";
                                                echo $upload_id->saveXML("uploadId.xml");
}
                        $account_key = 'RMTR';
                        $username = 'admin';
                        $password = 'Q8$Vaqn?e';
                        $file = 'RM500K.csv';
                        rm_largeFileUpload($account_key, $username, $password, $file);
?>