const callouts = [
    {
        name: 'Hardware',
        description: 'Op\u00e7\u00f5es de hardware para seu setup',
        imageSrc: './public/hardware.jpg',
        imageAlt: 'Desk with leather desk pad, walnut desk organizer, wireless keyboard and mouse, and porcelain mug.',
        href: '/hardware',
    },
    {
        name: 'Perif\u00e9ricos',
        description: 'Perif\u00e9ricos para inovar',
        imageSrc: './public/perifericos.jpg',
        imageAlt: 'Wood table with porcelain mug, leather journal, brass pen, leather key ring, and a houseplant.',
        href: '/periferico',
    },
    {
        name: 'Computadores',
        description: 'Computadores de alta tecnologia',
        imageSrc: './public/computadores.jpg',
        imageAlt: 'Collection of four insulated travel bottles on wooden shelf.',
        href: '/computadores',
    },
    {
        name: 'Games',
        description: 'Se\u00e7\u00e3o de games',
        imageSrc: './public/games.jpg',
        imageAlt: 'Collection of four insulated travel bottles on wooden shelf.',
        href: '/games',
    }
]

export default function Collections() {
    return (
        <div className="bg-gray-100">
            <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                <div className="mx-auto max-w-2xl py-16 sm:py-24 lg:max-w-none lg:py-32">
                    <h2 className="text-2xl font-bold text-gray-900">Categorias</h2>

                    <div className="mt-6 space-y-12 lg:grid lg:grid-cols-3 lg:gap-x-6 lg:space-y-0">
                        {callouts.map((callout) => (
                            <div key={callout.name} className="group relative">
                                <div className="relative h-80 w-full overflow-hidden rounded-lg bg-white sm:aspect-h-1 sm:aspect-w-2 lg:aspect-h-1 lg:aspect-w-1 group-hover:opacity-75 sm:h-64">
                                    <img
                                        src={callout.imageSrc}
                                        alt={callout.imageAlt}
                                        className="h-full w-full object-cover object-center"
                                    />
                                </div>
                                <h3 className="mt-6 text-sm text-gray-500">
                                    <a href={callout.href}>
                                        <span className="absolute inset-0" />
                                        {callout.name}
                                    </a>
                                </h3>
                                <p className="text-base font-semibold text-gray-900">{callout.description}</p>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    )
}
