### Time Defender
A game jam project based on the theme of time is your power. In short, a defense game using artificial intelligence. You can click on the [itch io](https://xox-games.itch.io/time-defender) link to try it on your browser.  Made in 3 days by using Unity mlagents.  
### Time Defender Lore
 Welcome, brave Time Defender!
 In the temple of time, it's up to you to protect the flow of time and save the world from chaos! In this magical universe, as a defender who rules time and space, your mission is to lead the Time Guardians and defend time against the undeads.
 If the undeads succeed in stealing time, they will come back to life and the balance of the universe will be destroyed. The fate of the universe is in your hands. Don't let this discourage you, brave defender! You can start the defense all over again by taking back time.
 Are you ready to become the master of time and save this fantastic world? Adventure awaits you!

### Contributors

Ayşenur Çavuş : https://www.linkedin.com/in/aysenurcavus24/

Dinçer Can Elitok : https://www.linkedin.com/in/dincer-can-elitok/

Esad Kibar : https://www.linkedin.com/in/esadkibar

Onur Küçük: https://www.linkedin.com/in/onurkck8/

Özgün Öykü Elçitepe : https://www.linkedin.com/in/OzgunOykuElcitepe
 ### If you wanna train your own ai model!

1. Go to the project folder in terminal or cmd. Make sure your active python version is 3.9.13. Create virtual environment.
```
python -m venv venv
```
2. Activate virtual environment.
```
venv\scripts\activate
```
3. Install/upgrade pip
```
python -m pip install --upgrade pip
```
4. Install mlagents library
```
pip install mlagents
```
5. Install torch libraries
```
pip3 install torch torchvision torchaudio
```
6. Fix protobuf version
```
pip install protobuf==3.20.3
```
7. Install onnx package
```
pip install onnx
```
8. Make sure mlagetns is installed correctly
 ```
mlagents-learn -h
```

If you see mlagents help text well done!

Whenever you want, you can activate venv like in step 2, then start train your model with
 ```
mlagents-learn --run-id=test
```

Of course you need to set up your scene for training and write some code to train your ai model.
This guide is for installing the python mlagents library. You can check the DefenderAgent script in Asset\Scripts folder to get an idea.

