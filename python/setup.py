from setuptools import setup, find_packages

setup(
    name="reachmail_api",
    version="1.3",
    license="GPLv3",
    description="ReachMail API python wrapper",
    url="https://github.com/ReachmailInc/WebApiSamples",
    author="Dan Nielsen / ReachMail, Inc.",
    author_email="dnielsen@reachmail.com",
    packages=find_packages(),
    install_requires=["httplib2"]
)
