# SatTalker

Client for communicating Frequency, Azimuth, and Elevation from Orbitron to an ICOM 9100 transceiver and a Yaseu G-5500 rotator.

Orbitron: http://www.stoff.pl/downloads.php

ICOM 9100: http://icomamerica.com/en/products/amateur/hf/9100/default.aspx

Yaesu G-550: http://www.yaesu.com/jp/en/products/ama_cpro.html

This program takes the frequency, azimuth, and elevation that is provided by Orbitron over DDE and uses it to automatically adjust the antenna rotator and the transceiver for a satellite's position and the doppler shift of its frequency due to its orbital speed.

This program went through several evolutions. It started out using a different DDE driver (the MyDDE driver on the Orbitron download page) that would write to a file and this program would read it. Horribly unsafe, and I hated doing it, but I was still learning C# at the time and MyDDE was written in Delphi, which I only had a 30 day free license for. So I went for a stopgap solution so that the club could still automatically track satellites. I came back to it a little while later and found the NDde library and created my own DDE driver to use with this software.  

This project will look like it has a bunch of TXT and HTML files. That is because I have Orbitron included in the project and it needs a bunch of these TXT and HTML files to run.
