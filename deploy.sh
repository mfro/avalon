# Deploy over SSH

cd $(dirname "${BASH_SOURCE[0]}")
DIR=$PWD

host=${2%:*}
path=${2#*:}

usage()
{
    echo "Usage:"
    echo " deploy.sh backend <user@host:path>"
    echo " deploy.sh frontend <user@host:path>"
    exit 1
}

if [ "$#" -ne 2 ]; then
    usage
elif [ "$1" = "frontend" ]; then
    echo "Deploying frontend to $host at $path"

    cd "./files"
    npm run build

    tar -czf "$DIR/deploy.tar.gz" -C "./build" .

elif [ "$1" = "backend" ]; then
    echo "Deploying backend to $host at $path"

    cd "./src/Mfroehlich.Avalon"
    dotnet publish -c Release -o "./deploy"
    tar -czf "$DIR/deploy.tar.gz" -C "./deploy" .

    rm -r "./deploy"

else
    usage
fi

cd $DIR

scp "deploy.tar.gz" $host:"deploy.tar.gz"
rm deploy.tar.gz

ssh -To ClearAllForwardings=yes $host << EOF
    rm -rf $path

    mkdir -p $path

    tar -xf deploy.tar.gz -C $path

    rm deploy.tar.gz
EOF

echo "Complete"